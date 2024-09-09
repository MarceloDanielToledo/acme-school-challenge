
# Desafio: Prueba de concepto para escuela ACME



## Autores

- [@marcelodanieltoledo](https://github.com/MarceloDanielToledo)


## Contexto

Se propone como desafio técnico realizar un PoC (Prueba de concepto) para una escuela que necesita ayuda con la gestión de sus cursos y estudiantes.

La solución se realizo en una aplicación Blazor WebAssembly para otorgar una interfaz del prototipo, añadiendo componentes de la libreria Mudblazor.

El sistema presentado permite registrar estudiantes y cursos, inscribir estudiantes en cursos, y generar listas de cursos y estudiantes en un rango de fechas específico. La solución está diseñada para ser extensible, con abstracciones que permiten futuras mejoras, incluyendo la integración con APIs y bases de datos.


## Modelo
![Modelo](https://i.imgur.com/5W5nLmy.png)

## Arquitectura
![Arquitectura](https://miro.medium.com/v2/resize:fit:640/format:webp/1*0Pg6_UsaKiiEqUV3kf2HXg.png)

La estructura de la solución se hizo siguiendo la arquitectura Onion, añadiendo una capa de servicios compartidos (Shared) y otra para mantener el modelado del gateway de pagos para una posible exportación del mismo hacia una nueva solución en el futuro.

## Patrones y librerias utilizados

 - CQRS para separar la escritura y lectura en la aplicación, utilizando IMediator.
 - Result Pattern para encapsular todos los resultados de las operaciones de la aplicación en un solo objeto.
 - Screaming Architecture para la organización de carpetas en la capa de aplicación.
 - Pattern Marching para simplificar el mapeo entre distintos objetos.
 - Auditing Pattern para registrar información sobre la creación y modificación de datos.

### Principales librerías
- MediaTr para simplificar la ejecución de comandos y queries utilizando IMediator.
- Automapper para simplificar las reglas de mapeo entre objetos.
- Ardalis para crear consultas reutilizables de manera más expresiva.
- Microsoft.Extensions.DependencyInjection para gestionar de una manera más modular las inyecciones de dependencias de las distintas capas de la aplicación.
- Microsoft.EntityFrameworkCore.InMemory para trabajar sobre una base de datos en memoria.
- Mudblazor para reutilizar componentes Blazor basados en Material Design.
- FluentValidation para añadir reglas de validación a objetos más descriptivas.
- xUnit para realizar los tests necesarios.
- Moq para verificar el comportamiento de una dependencia durante un test.
## Respuestas del desafío
- ¿Qué cosas te hubiera gustado hacer pero no hiciste?
Me hubiera gustado poder comunicarme con una pasarela de pagos real (en su ambiente de test) para poder trabajar con un servicio externo y contemplar aspectos interesantes como la resiliencia, manejo de errores en la comunicación.
También me hubiera gustado separar la aplicación en microservicios, añadiendo un gateway de autenticación/autorización junto con un proveedor como IdentityServer, un servicio de mensajeria para el manejo de solicitudes de pagos/inscripciones con sus respectivas API para el procesamiento.


- ¿Qué cosas hiciste pero crees que podrían mejorarse o serían necesarias revisar si el proyecto avanza?
Una de las cosas más importante que considero a mejorar en caso de que avance el proyecto seria el manejo de excepciones de la aplicación, que podría hacerse mediante un Middleware, por ejemplo.


- ¿Cuánto tiempo has invertido en hacer el proyecto? ¿Qué cosas tuviste que investigar? ¿Qué cosas eran nuevas para ti?
Teniendo en cuenta el diseño y desarrollo de la solución, apróximadamente 13/15 hs.
Tuve que leer la documentación de Mublazor acerca de los eventos de algunos componentes en particular.

## ¿Cómo correr la aplicación?
- Ejecuta la aplicación a través de Visual Studio abriendo el archivo .sln. Establece Acme.School.UI como el proyecto de inicio predeterminado.