using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using Taskflow.API.CustomHealthCheck;
using Taskflow.Application;
using Taskflow.Application.ResponseDto.Common;
using Taskflow.Application.Sequences;
using Taskflow.Application.Utilities;
using Taskflow.Domain.ModelsPortal;

namespace Taskflow.API.Config
{
    public static class ServicesConfig
    {
        public static void AddConfig(this IServiceCollection services, IConfiguration configuration)
        {
            // Otros servicios de extensión de paquetes NuGet
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(allowedOrigins)
                        .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                        .WithHeaders("Authorization", "Content-Type", "token");
                });
            });

            services.AddSwagger();
            services.AddJwt();
            services.AddExternalServices();
            services.AddInternalServices();
            services.BindAppSettings(configuration);

            // Configuración de auto mapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Configuración de la base de datos
            services.AddDbContext<PortalContext>(options => options.UseOracle(configuration.GetConnectionString("Portal")));

            // Configuración de Health Checks
            services.AddHealthChecks(configuration);

            services.AddControllers();

            services.Configure<KestrelServerOptions>(options => { options.Limits.MaxRequestBodySize = 5242880; });

            //Política de autorización
            services.AddAuthorizationBuilder()
                .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build());
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.AddConfiguration(configuration);
        }

        private static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            Sequence.Initialize(configuration);
            var portalDbContext = services.BuildServiceProvider().GetRequiredService<PortalContext>();
            UserAuditoria.Initialize(portalDbContext);
        }

        private static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de Health Checks
            services.AddHealthChecks()
                .AddCheck<DatabasePortalHealtCheck>(nameof(DatabasePortalHealtCheck));
        }

        private static void AddSwagger(this IServiceCollection services)
        {
            // Acá se añade la implementación del Swagger
            services.AddSwaggerGen(c =>
            {
                var title = "API RESTful TaskFlow";
                c.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = "v1" });

                // Agrega la configuración de seguridad para el Bearer Token
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Ingrese el token de autenticación en el siguiente formato: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                });

                // Agrega la configuración de seguridad requerida para acceder a los endpoints
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        Array.Empty<string>()
                    },
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
        }

        private static void AddJwt(this IServiceCollection services)
        {
            _ = services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = AppSettings.Jwt.Issuer,
                    ValidAudience = AppSettings.Jwt.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.Jwt.SecretKey)),
                    ClockSkew = TimeSpan.Zero,
                };

                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        return context.Response.WriteAsync(JsonSerializer.Serialize(
                            OperationResponse<object>.CreateBuilder().WithCode(401)
                                .WithMessage(MsjResponse.MsjNotAuthenticate).Build()));
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync(JsonSerializer.Serialize(OperationResponse<object>
                            .CreateBuilder().WithCode(403)
                            .WithMessage(MsjResponse.MsjUnauthorized).Build()));
                    },
                };
            });
        }

        private static void AddInternalServices(this IServiceCollection services)
        {
            // Acá se añade el ciclo de vida de los servicios
            //   services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IHealthCheck, DatabasePortalHealtCheck>();
        }

        private static void AddExternalServices(this IServiceCollection services)
        {
            // Acá se añade el ciclo de vida de los servicios externos
        }

        private static void BindAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            // Se bindean los datos del appsettings.json hacia la clase AppSettings
            var dataBase = new AppSettings.DataBase();
            var jwt = new AppSettings.Jwt();
            var sequences = new AppSettings.Sequences();
            var smtp = new AppSettings.Smtp();
            var sendMail = new AppSettings.SendMail();

            configuration.Bind("ConnectionStrings", dataBase);
            configuration.Bind("JWT", jwt);
            configuration.Bind("Sequences", sequences);
            configuration.Bind("Smtp", smtp);
            configuration.Bind("SendMail", sendMail);
        }

    }
}
