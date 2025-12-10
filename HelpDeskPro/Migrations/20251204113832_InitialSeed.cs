using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskPro.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // === USERS ============================================================
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[]
                {
                    "Id", "Email", "PasswordHash", "FirstName", "LastName",
                    "Role", "IsActive", "CreatedAt", "LastLoginAt"
                },
                values: new object[,]
                {
                    { 1, "admin1@helpdeskpro.local",   "HASH_ADMIN1", "Alice",   "Admin",    "Admin",    true, new DateTime(2025,1,1,8,0,0,DateTimeKind.Utc),  null },
                    { 2, "admin2@helpdeskpro.local",   "HASH_ADMIN2", "Bob",     "Root",     "Admin",    true, new DateTime(2025,1,2,8,0,0,DateTimeKind.Utc),  null },

                    { 3, "agent1@helpdeskpro.local",   "HASH_AGENT1", "Carlos",  "Soto",     "Agent",    true, new DateTime(2025,1,3,8,0,0,DateTimeKind.Utc),  null },
                    { 4, "agent2@helpdeskpro.local",   "HASH_AGENT2", "Diana",   "Ruiz",     "Agent",    true, new DateTime(2025,1,4,8,0,0,DateTimeKind.Utc),  null },
                    { 5, "agent3@helpdeskpro.local",   "HASH_AGENT3", "Eduardo", "López",   "Agent",    true, new DateTime(2025,1,5,8,0,0,DateTimeKind.Utc),  null },
                    { 6, "agent4@helpdeskpro.local",   "HASH_AGENT4", "Laura",   "García",   "Agent",    true, new DateTime(2025,1,6,8,0,0,DateTimeKind.Utc),  null },

                    { 7, "customer1@helpdeskpro.local","HASH_CUST1",  "María",   "Pérez",    "Customer", true, new DateTime(2025,1,7,8,0,0,DateTimeKind.Utc),  null },
                    { 8, "customer2@helpdeskpro.local","HASH_CUST2",  "Jorge",   "Martín",   "Customer", true, new DateTime(2025,1,8,8,0,0,DateTimeKind.Utc),  null },
                    { 9, "customer3@helpdeskpro.local","HASH_CUST3",  "Lucía",   "Serrano",  "Customer", true, new DateTime(2025,1,9,8,0,0,DateTimeKind.Utc),  null },
                    { 10,"customer4@helpdeskpro.local","HASH_CUST4",  "Iván",    "Corral",   "Customer", true, new DateTime(2025,1,10,8,0,0,DateTimeKind.Utc), null }
                });

            // === TEAMS ============================================================
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name", "Description", "IsActive", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "Soporte Nivel 1", "Primer nivel de soporte y filtrado de tickets.", true, new DateTime(2025,1,1,9,0,0,DateTimeKind.Utc) },
                    { 2, "Soporte Nivel 2", "Segundo nivel, incidencias complejas.",          true, new DateTime(2025,1,1,9,5,0,DateTimeKind.Utc) },
                    { 3, "Infraestructura", "Redes, servidores, acceso remoto.",              true, new DateTime(2025,1,1,9,10,0,DateTimeKind.Utc) }
                });

            // === TEAM MEMBERS (N:N) ==============================================
            migrationBuilder.InsertData(
                table: "TeamMembers",
                columns: new[] { "TeamId", "UserId" },
                values: new object[,]
                {
                    { 1, 3 }, // agent1 -> L1
                    { 1, 4 }, // agent2 -> L1
                    { 2, 5 }, // agent3 -> L2
                    { 3, 6 }  // agent4 -> Infra
                });

            // === TICKET CATEGORIES ===============================================
            migrationBuilder.InsertData(
                table: "TicketCategories",
                columns: new[] { "Id", "Name", "Description", "IsActive", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "Hardware", "Incidencias de PCs, portátiles, monitores, etc.", true, new DateTime(2025,1,1,10,0,0,DateTimeKind.Utc) },
                    { 2, "Software", "Aplicaciones, sistema operativo, ofimática.",     true, new DateTime(2025,1,1,10,5,0,DateTimeKind.Utc) },
                    { 3, "Red",      "Conectividad, VPN, WiFi, cableado.",              true, new DateTime(2025,1,1,10,10,0,DateTimeKind.Utc) },
                    { 4, "Acceso",   "Problemas de credenciales o permisos.",           true, new DateTime(2025,1,1,10,15,0,DateTimeKind.Utc) },
                    { 5, "Otros",    "Cualquier incidencia general.",                   true, new DateTime(2025,1,1,10,20,0,DateTimeKind.Utc) }
                });

            // === TICKET TAGS =====================================================
            migrationBuilder.InsertData(
                table: "TicketTags",
                columns: new[] { "Id", "Name", "Color", "IsActive", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "Producción", "#FF0000", true, new DateTime(2025,1,1,11,0,0,DateTimeKind.Utc) },
                    { 2, "Urgente",    "#FF4500", true, new DateTime(2025,1,1,11,5,0,DateTimeKind.Utc) },
                    { 3, "Seguridad",  "#0066FF", true, new DateTime(2025,1,1,11,10,0,DateTimeKind.Utc) },
                    { 4, "Cliente VIP","#FFD700", true, new DateTime(2025,1,1,11,15,0,DateTimeKind.Utc) },
                    { 5, "Bug",        "#800080", true, new DateTime(2025,1,1,11,20,0,DateTimeKind.Utc) },
                    { 6, "Mejora",     "#008000", true, new DateTime(2025,1,1,11,25,0,DateTimeKind.Utc) }
                });

            // === TICKETS =========================================================
            // Status (int): 0 = Open, 1 = InProgress, 2 = WaitingCustomer, 3 = Resolved, 4 = Closed
            // Priority (int): 0 = Low, 1 = Normal, 2 = High, 3 = Critical
            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[]
                {
                    "Id", "Code", "Title", "Description",
                    "Status", "Priority",
                    "CategoryId", "ReporterId", "AssigneeId", "TeamId",
                    "CreatedAt", "UpdatedAt", "ResolvedAt", "ClosedAt", "DueAt",
                    "IsDeleted"
                },
                values: new object[,]
                {
                    {
                        1, "HD-000001", "No puedo acceder al correo",
                        "Desde esta mañana, Outlook pide credenciales y no acepta ninguna.",
                        0, 2, // Open, High
                        2, 7, 3, 1,
                        new DateTime(2025,1,2,8,30,0,DateTimeKind.Utc),
                        new DateTime(2025,1,2,8,35,0,DateTimeKind.Utc),
                        null, null,
                        new DateTime(2025,1,3,8,30,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        2, "HD-000002", "El PC no enciende",
                        "El equipo de sobremesa se queda con la luz de encendido fija pero no muestra imagen.",
                        1, 3, // InProgress, Critical
                        1, 8, 4, 1,
                        new DateTime(2025,1,2,9,0,0,DateTimeKind.Utc),
                        new DateTime(2025,1,2,10,0,0,DateTimeKind.Utc),
                        null, null,
                        new DateTime(2025,1,2,17,0,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        3, "HD-000003", "No tengo acceso a la VPN",
                        "Intento conectarme a la VPN corporativa y da error de credenciales.",
                        2, 2, // WaitingCustomer, High
                        3, 7, 5, 3,
                        new DateTime(2025,1,3,7,45,0,DateTimeKind.Utc),
                        new DateTime(2025,1,3,8,10,0,DateTimeKind.Utc),
                        null, null,
                        new DateTime(2025,1,4,7,45,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        4, "HD-000004", "La red va muy lenta",
                        "La navegación web y las carpetas de red tardan mucho en cargar.",
                        1, 1, // InProgress, Normal
                        3, 9, 3, 1,
                        new DateTime(2025,1,4,9,15,0,DateTimeKind.Utc),
                        new DateTime(2025,1,4,9,45,0,DateTimeKind.Utc),
                        null, null,
                        new DateTime(2025,1,5,9,15,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        5, "HD-000005", "Error en la aplicación de facturación",
                        "Al generar una factura salta una excepción y se cierra la app.",
                        0, 3, // Open, Critical
                        2, 10, 5, 2,
                        new DateTime(2025,1,5,10,0,0,DateTimeKind.Utc),
                        new DateTime(2025,1,5,10,5,0,DateTimeKind.Utc),
                        null, null,
                        new DateTime(2025,1,6,10,0,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        6, "HD-000006", "Solicitud de nuevo usuario",
                        "Nuevo empleado en el departamento de ventas, necesita cuenta de AD y correo.",
                        3, 1, // Resolved, Normal
                        4, 8, 4, 2,
                        new DateTime(2025,1,6,8,0,0,DateTimeKind.Utc),
                        new DateTime(2025,1,6,9,30,0,DateTimeKind.Utc),
                        new DateTime(2025,1,6,9,15,0,DateTimeKind.Utc),
                        null,
                        new DateTime(2025,1,7,8,0,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        7, "HD-000007", "Solicitud de mejora en CRM",
                        "Sería útil poder filtrar los clientes por fecha de última compra.",
                        0, 0, // Open, Low
                        5, 7, 6, 2,
                        new DateTime(2025,1,7,11,0,0,DateTimeKind.Utc),
                        new DateTime(2025,1,7,11,0,0,DateTimeKind.Utc),
                        null, null,
                        new DateTime(2025,1,14,11,0,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        8, "HD-000008", "Corte de red en planta 2",
                        "Nadie en la planta 2 puede acceder a Internet ni a las carpetas compartidas.",
                        3, 3, // Resolved, Critical
                        3, 9, 3, 3,
                        new DateTime(2025,1,8,7,30,0,DateTimeKind.Utc),
                        new DateTime(2025,1,8,9,0,0,DateTimeKind.Utc),
                        new DateTime(2025,1,8,8,45,0,DateTimeKind.Utc),
                        null,
                        new DateTime(2025,1,8,12,0,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        9, "HD-000009", "El portátil se sobrecalienta",
                        "El ventilador hace ruido constante y el portátil se calienta mucho.",
                        1, 1, // InProgress, Normal
                        1, 10, 3, 1,
                        new DateTime(2025,1,9,8,45,0,DateTimeKind.Utc),
                        new DateTime(2025,1,9,9,30,0,DateTimeKind.Utc),
                        null, null,
                        new DateTime(2025,1,10,8,45,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        10, "HD-000010", "Error de login en intranet",
                        "Mensaje 'usuario o contraseña incorrectos' aunque las credenciales son válidas.",
                        2, 2, // WaitingCustomer, High
                        4, 8, 5, 2,
                        new DateTime(2025,1,10,9,0,0,DateTimeKind.Utc),
                        new DateTime(2025,1,10,9,20,0,DateTimeKind.Utc),
                        null, null,
                        new DateTime(2025,1,11,9,0,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        11, "HD-000011", "Petición de acceso a carpeta de departamento",
                        "Necesito acceso a la carpeta de contabilidad para consultar informes.",
                        3, 1, // Resolved, Normal
                        4, 7, 4, 2,
                        new DateTime(2025,1,11,10,0,0,DateTimeKind.Utc),
                        new DateTime(2025,1,11,11,0,0,DateTimeKind.Utc),
                        new DateTime(2025,1,11,10,30,0,DateTimeKind.Utc),
                        null,
                        new DateTime(2025,1,12,10,0,0,DateTimeKind.Utc),
                        false
                    },
                    {
                        12, "HD-000012", "Aplicación de informes se cierra sola",
                        "Al abrir un informe grande, la aplicación se cierra sin mensaje.",
                        0, 2, // Open, High
                        2, 9, 5, 2,
                        new DateTime(2025,1,12,8,15,0,DateTimeKind.Utc),
                        new DateTime(2025,1,12,8,20,0,DateTimeKind.Utc),
                        null, null,
                        new DateTime(2025,1,13,8,15,0,DateTimeKind.Utc),
                        false
                    }
                });

            // === TICKET TAG ASSIGNMENTS (N:N) ====================================
            migrationBuilder.InsertData(
                table: "TicketTagAssignments",
                columns: new[] { "TicketId", "TicketTagId" },
                values: new object[,]
                {
                    { 1, 2 }, // Urgente
                    { 1, 5 }, // Bug
                    { 2, 1 }, // Producción
                    { 2, 2 }, // Urgente
                    { 3, 3 }, // Seguridad
                    { 4, 1 }, // Producción
                    { 5, 1 }, // Producción
                    { 5, 5 }, // Bug
                    { 6, 4 }, // Cliente VIP
                    { 7, 6 }, // Mejora
                    { 8, 1 }, // Producción
                    { 8, 2 }, // Urgente
                    { 9, 1 }, // Producción
                    { 10, 3 },// Seguridad
                    { 11, 4 },// Cliente VIP
                    { 12, 5 } // Bug
                });

            // === TICKET COMMENTS =================================================
            migrationBuilder.InsertData(
                table: "TicketComments",
                columns: new[]
                {
                    "Id", "TicketId", "AuthorId", "Body", "IsInternal", "CreatedAt"
                },
                values: new object[,]
                {
                    { 1, 1, 7,  "He probado a reiniciar el equipo y sigue ocurriendo.", false, new DateTime(2025,1,2,8,40,0,DateTimeKind.Utc) },
                    { 2, 1, 3,  "Revisando los logs del servidor de correo.",           true,  new DateTime(2025,1,2,8,50,0,DateTimeKind.Utc) },
                    { 3, 2, 8,  "El problema empezó después de una actualización.",    false, new DateTime(2025,1,2,9,10,0,DateTimeKind.Utc) },
                    { 4, 2, 4,  "Comprobando fuente de alimentación y RAM.",           true,  new DateTime(2025,1,2,10,5,0,DateTimeKind.Utc) },
                    { 5, 3, 7,  "Adjunto captura del error de la VPN.",                false, new DateTime(2025,1,3,7,50,0,DateTimeKind.Utc) },
                    { 6, 3, 5,  "Probando regenerar perfil de VPN para el usuario.",   true,  new DateTime(2025,1,3,8,15,0,DateTimeKind.Utc) },
                    { 7, 5, 10, "La app se cierra siempre al facturar más de 10 líneas.", false, new DateTime(2025,1,5,10,10,0,DateTimeKind.Utc) },
                    { 8, 5, 5,  "Identificado posible bug en el módulo de totales.",   true,  new DateTime(2025,1,5,10,30,0,DateTimeKind.Utc) },
                    { 9, 6, 8,  "Confirmo que el nuevo usuario ya puede iniciar sesión.", false, new DateTime(2025,1,6,9,40,0,DateTimeKind.Utc) },
                    { 10,7, 7,  "Es solo una sugerencia, no es urgente.",              false, new DateTime(2025,1,7,11,10,0,DateTimeKind.Utc) },
                    { 11,8, 9,  "La planta 3 no parece afectada, solo la 2.",          false, new DateTime(2025,1,8,7,35,0,DateTimeKind.Utc) },
                    { 12,8, 3,  "Incidencia ligada a switch principal de planta 2.",   true,  new DateTime(2025,1,8,8,30,0,DateTimeKind.Utc) },
                    { 13,9, 10, "El ventilador lleva así unas semanas.",               false, new DateTime(2025,1,9,8,50,0,DateTimeKind.Utc) },
                    { 14,9, 3,  "Limpiado interior, se recomienda cambiar la pasta térmica.", true, new DateTime(2025,1,9,9,40,0,DateTimeKind.Utc) },
                    { 15,10,8, "He cambiado la contraseña por si acaso y sigue fallando.", false, new DateTime(2025,1,10,9,10,0,DateTimeKind.Utc) },
                    { 16,10,5, "Revisando los logs de autenticación de la intranet.",  true,  new DateTime(2025,1,10,9,25,0,DateTimeKind.Utc) },
                    { 17,11,7, "Muchas gracias, ya veo los informes.",                 false, new DateTime(2025,1,11,11,10,0,DateTimeKind.Utc) },
                    { 18,12,9, "Parece que el fallo solo ocurre con algunos informes.", false, new DateTime(2025,1,12,8,25,0,DateTimeKind.Utc) }
                });

            // === TICKET ATTACHMENTS =============================================
            migrationBuilder.InsertData(
                table: "TicketAttachments",
                columns: new[]
                {
                    "Id", "TicketId", "FileName", "ContentType",
                    "FileSizeBytes", "StorageUrl", "UploadedById", "UploadedAt"
                },
                values: new object[,]
                {
                    { 1, 1, "outlook-error.png", "image/png",  245678, "https://storage.local/tickets/1/outlook-error.png", 7,  new DateTime(2025,1,2,8,38,0,DateTimeKind.Utc) },
                    { 2, 3, "vpn-error.png",     "image/png",  198234, "https://storage.local/tickets/3/vpn-error.png",     7,  new DateTime(2025,1,3,7,52,0,DateTimeKind.Utc) },
                    { 3, 5, "facturacion.log",   "text/plain", 45231,  "https://storage.local/tickets/5/facturacion.log",   10, new DateTime(2025,1,5,10,15,0,DateTimeKind.Utc) },
                    { 4, 8, "switch-config.txt", "text/plain", 10567,  "https://storage.local/tickets/8/switch-config.txt", 3,  new DateTime(2025,1,8,8,20,0,DateTimeKind.Utc) },
                    { 5, 9, "temperaturas.png",  "image/png",  223344, "https://storage.local/tickets/9/temperaturas.png",  10, new DateTime(2025,1,9,9,0,0,DateTimeKind.Utc) },
                    { 6, 12,"informes-crash.dmp","application/octet-stream", 512000, "https://storage.local/tickets/12/informes-crash.dmp", 9, new DateTime(2025,1,12,8,30,0,DateTimeKind.Utc) }
                });

            // === REFRESH TOKENS (ejemplo) =======================================
            migrationBuilder.InsertData(
                table: "RefreshTokens",
                columns: new[] { "Id", "UserId", "Token", "ExpiresAt", "CreatedAt", "RevokedAt" },
                values: new object[,]
                {
                    { 1, 1, "REFRESH_ADMIN1_TOKEN",  new DateTime(2025,2,1,0,0,0,DateTimeKind.Utc), new DateTime(2025,1,2,7,0,0,DateTimeKind.Utc), null },
                    { 2, 3, "REFRESH_AGENT1_TOKEN",  new DateTime(2025,2,1,0,0,0,DateTimeKind.Utc), new DateTime(2025,1,3,7,0,0,DateTimeKind.Utc), null },
                    { 3, 7, "REFRESH_CUSTOMER1_TOKEN", new DateTime(2025,2,1,0,0,0,DateTimeKind.Utc), new DateTime(2025,1,7,7,0,0,DateTimeKind.Utc), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Borramos en orden inverso a dependencias

            // Refresh tokens
            migrationBuilder.DeleteData(
                table: "RefreshTokens",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });

            // Attachments
            migrationBuilder.DeleteData(
                table: "TicketAttachments",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6 });

            // Comments
            migrationBuilder.DeleteData(
                table: "TicketComments",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1,2,3,4,5,6,7,8,9,
                    10,11,12,13,14,15,
                    16,17,18
                });

            // TicketTagAssignments (N:N)
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 1, 2 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 1, 5 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 2, 1 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 2, 2 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 3, 3 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 4, 1 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 5, 1 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 5, 5 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 6, 4 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 7, 6 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 8, 1 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 8, 2 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 9, 1 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 10, 3 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 11, 4 });
            migrationBuilder.DeleteData(
                table: "TicketTagAssignments",
                keyColumns: new[] { "TicketId", "TicketTagId" },
                keyValues: new object[] { 12, 5 });

            // TeamMembers (N:N)
            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumns: new[] { "TeamId", "UserId" },
                keyValues: new object[] { 1, 3 });
            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumns: new[] { "TeamId", "UserId" },
                keyValues: new object[] { 1, 4 });
            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumns: new[] { "TeamId", "UserId" },
                keyValues: new object[] { 2, 5 });
            migrationBuilder.DeleteData(
                table: "TeamMembers",
                keyColumns: new[] { "TeamId", "UserId" },
                keyValues: new object[] { 3, 6 });

            // Tickets
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1,2,3,4,5,6,
                    7,8,9,10,11,12
                });

            // TicketTags
            migrationBuilder.DeleteData(
                table: "TicketTags",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6 });

            // TicketCategories
            migrationBuilder.DeleteData(
                table: "TicketCategories",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5 });

            // Teams
            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3 });

            // Users
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1,2,3,4,5,6,
                    7,8,9,10
                });
        }
    }
}
