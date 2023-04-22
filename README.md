# AzureADB2C
Aplicación para gestionar Usuarios de Azure AD B2C. Usamos el paquete Microsoft.Graph.
Operaciones Disponibles:
- Listar Usuarios
- Obtener Usuario por ID
- Obtener Usuario por SignInName
- Eliminar Usuario por ID
- Crear Usuarios

Se debe configurar las credenciales del Azure AD B2C en al archivo appsettings.json.

"B2C": {
    "TenantId": "learnsmartcoding.onmicrosoft.com",
    "AppId": "9eefb36e-7a90-48b5-9488-ffe6f4e855a9",
    "ClientSecret": "KXN8Q~ar6~8s1PUNQ-9762ic8sfCLg1wrc53LdoU",
    "B2cExtensionAppClientId": "dd13c9c7-6e13-47b3-abc3-b531518b8ae9"
  }
