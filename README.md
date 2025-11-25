# 📞 ContactPhoneLogPlugin  <br>
<img src="./logo.svg" ancho="420"/>
_Auditoría avanzada de cambios en teléfono para Contactos en Microsoft Dataverse_

## 🧩 Descripción

**ContactPhoneLogPlugin** es un plugin para Microsoft Dataverse/Dynamics 365 que registra automáticamente un historial cada vez que se actualiza el número de teléfono principal (`telephone1`) de un contacto.  

El plugin crea un registro en una entidad personalizada (`new_contactlog`) guardando:

- Teléfono anterior  
- Teléfono nuevo  
- Fecha del cambio  
 

Esto permite auditoría interna, trazabilidad y análisis de cambios en datos sensibles.

---

## 🚀 Características principales

✔ Registra todos los cambios de `telephone1` en PostOperation  
✔ Utiliza Pre Image y Post Image correctamente configuradas  
✔ Manejo seguro de nulls y validaciones  
✔ Código optimizado y preparado para entornos productivos  
✔ Trazas detalladas para diagnóstico en tiempo real  

---

## 🛠 Arquitectura del Plugin

### **Evento**
| Propiedad | Valor |
|----------|-------|
| **Mensaje** | Update |
| **Entidad** | contact |
| **Stage** | PostOperation (40) |
| **Modo** | Synchronous |
| **Filtro** | telephone1 |
| **Imágenes** | PreImage (telephone1), PostImage (telephone1) |

---

## 📦 Estructura del Proyecto
/ContactPhoneLogPlugin
│── ContactPhoneLogPlugin.cs
│── ContactPhoneLogPlugin.sln
│── Propiedades/
└── README.md
## 🧠 Flujo de ejecución

1. El usuario actualiza `telephone1` en un contacto.  
2. El plugin detecta el cambio usando **Filtering Attributes**.  
3. Se lee el valor anterior desde **Pre Image**.  
4. Se lee el valor nuevo desde **Post Image**.  
5. Se crea un registro en `new_contactlog` con la información del cambio.  
6. El log queda disponible para auditoría o reportes.

7. | Prueba                   | Resultado esperado         |
| ------------------------ | -------------------------- |
| Cambiar telephone1       | Se crea un log con old/new |
| Cambiar otro campo       | No ejecuta plugin          |
| telephone1 vacío → valor | Se registra correctamente  |
| valor → vacío            | Se registra “nuevo vacío”  |


📊 Diagrama UML (Secuencia)

sequenceDiagram
    participant User
    participant Dataverse
    participant Plugin
    participant LogEntity

    User->>Dataverse: Update contact (telephone1)
    Dataverse->>Plugin: Ejecutar PostOperation
    Plugin->>Plugin: Leer PreImage.telephone1
    Plugin->>Plugin: Leer PostImage.telephone1
    Plugin->>LogEntity: Crear registro new_contactlog
    LogEntity-->>Plugin: Registro creado
    Plugin-->>Dataverse: Finaliza ejecución
📝 Requisitos

.NET Framework 4.6.2+

Microsoft.CrmSdk.CoreAssemblies <br>
<img width="342" height="63" alt="image" src="https://github.com/user-attachments/assets/f13c3741-f443-4ae7-835b-7faf26292a05" /><br>
<img width="353" height="335" alt="image" src="https://github.com/user-attachments/assets/acaa50ab-69c7-4723-af95-f348300e806f" /> <br>
<img width="420" height="127" alt="image" src="https://github.com/user-attachments/assets/41332238-3d08-4e32-833c-d417db630e31" /> <br>
<img width="419" height="122" alt="image" src="https://github.com/user-attachments/assets/7f62b5a4-7623-4ab2-93a0-1eddf07c78be" /> <br>

Entidad personalizada new_contactlogcon campos:<br>
<img width="362" height="382" alt="image" src="https://github.com/user-attachments/assets/5e25627d-942c-4029-ba29-7c26e8e7ec6e" />

new_contact(Buscar)

new_oldphone(Texto)

new_newphone(Texto)

new_description(Texto)

🤝 Contribuciones

Pull request y sugerencias son bienvenidas.

📄 Licencia

Este proyecto se distribuye bajo licencia MIT.

👨‍💻 Autor

Jeisson Triana
Desarrollador Dynamics 365 / Power Platform
GitHub: https://github.com/Jtriana659
