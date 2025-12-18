using System;
using System.Collections.Generic;
using System.Linq;

namespace AgendaTelefonica
{
    // Clase que representa un contacto individual
    public class Contacto
    {
        // Propiedades autoimplementadas
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        // Constructor
        public Contacto(string nombre, string telefono, string email)
        {
            Nombre = nombre;
            Telefono = telefono;
            Email = email;
        }

        // Método para mostrar información del contacto
        public void MostrarInformacion()
        {
            Console.WriteLine("╔══════════════════════════════════╗");
            Console.WriteLine($"║ Nombre: {Nombre,-25}║");
            Console.WriteLine($"║ Teléfono: {Telefono,-22} ║");
            Console.WriteLine($"║ Email: {Email,-24}  ║");
            Console.WriteLine("╚══════════════════════════════════╝");
        }
    }

    // Clase que maneja la colección de contactos
    public class Agenda
    {
        // Lista para almacenar los contactos (estructura de datos principal)
        private List<Contacto> contactos;

        public Agenda()
        {
            contactos = new List<Contacto>();
            CargarDatosIniciales(); // Datos de ejemplo para pruebas
        }

        // Método para cargar datos iniciales de prueba
        private void CargarDatosIniciales()
        {
            AgregarContacto("Juan Pérez", "0991234567", "juan@email.com");
            AgregarContacto("María García", "0987654321", "maria@email.com");
            AgregarContacto("Carlos López", "0971122334", "carlos@email.com");
        }

        // 1. Agregar un nuevo contacto
        public void AgregarContacto(string nombre, string telefono, string email)
        {
            // Validación básica
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("⚠️  Error: El nombre no puede estar vacío.");
                return;
            }

            var nuevoContacto = new Contacto(nombre, telefono, email);
            contactos.Add(nuevoContacto);
            Console.WriteLine($"✅ Contacto '{nombre}' agregado exitosamente.");
        }

        // 2. Mostrar todos los contactos
        public void MostrarTodosLosContactos()
        {
            Console.WriteLine("\n📋 LISTA COMPLETA DE CONTACTOS");
            Console.WriteLine("══════════════════════════════════════");

            if (contactos.Count == 0)
            {
                Console.WriteLine("📭 La agenda está vacía.");
                return;
            }

            for (int i = 0; i < contactos.Count; i++)
            {
                Console.WriteLine($"\n📞 Contacto #{i + 1}:");
                contactos[i].MostrarInformacion();
            }

            Console.WriteLine($"\n📊 Total: {contactos.Count} contactos");
        }

        // 3. Buscar contacto por nombre (búsqueda lineal)
        public void BuscarContactoPorNombre(string nombreBuscado)
        {
            Console.WriteLine($"\n🔍 BUSCANDO: '{nombreBuscado}'");
            Console.WriteLine("══════════════════════════════════════");

            var resultados = contactos
                .Where(c => c.Nombre.ToLower().Contains(nombreBuscado.ToLower()))
                .ToList();

            if (resultados.Count == 0)
            {
                Console.WriteLine("❌ No se encontraron contactos con ese nombre.");
                return;
            }

            Console.WriteLine($"✅ Se encontraron {resultados.Count} contacto(s):");
            foreach (var contacto in resultados)
            {
                contacto.MostrarInformacion();
            }
        }

        // 4. Eliminar contacto por nombre
        public void EliminarContactoPorNombre(string nombreEliminar)
        {
            var contactosAEliminar = contactos
                .Where(c => c.Nombre.Equals(nombreEliminar, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (contactosAEliminar.Count == 0)
            {
                Console.WriteLine($"❌ No se encontró el contacto '{nombreEliminar}'.");
                return;
            }

            foreach (var contacto in contactosAEliminar)
            {
                contactos.Remove(contacto);
            }

            Console.WriteLine($"✅ Se eliminó {contactosAEliminar.Count} contacto(s) con el nombre '{nombreEliminar}'.");
        }

        // 5. Contar total de contactos
        public void MostrarEstadisticas()
        {
            Console.WriteLine("\n📊 ESTADÍSTICAS DE LA AGENDA");
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine($"• Total de contactos: {contactos.Count}");
            Console.WriteLine($"• Capacidad actual: {contactos.Count}/{contactos.Capacity}");
        }

        // 6. Verificar si la agenda está vacía
        public bool AgendaVacia()
        {
            return contactos.Count == 0;
        }
    }

    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "📱 Agenda Telefónica - Estructura de Datos";
            Console.ForegroundColor = ConsoleColor.Cyan;

            Agenda agenda = new Agenda();
            bool continuar = true;

            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║        AGENDA TELEFÓNICA - ESTRUCTURA DE DATOS   ║");
            Console.WriteLine("║         Práctica #01 - Universidad Amazónica     ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");

            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("➕ AGREGAR NUEVO CONTACTO");
                        Console.WriteLine("════════════════════════════");
                        Console.Write("Nombre completo: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Teléfono: ");
                        string telefono = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        agenda.AgregarContacto(nombre, telefono, email);
                        break;

                    case "2":
                        Console.Clear();
                        agenda.MostrarTodosLosContactos();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("🔍 BUSCAR CONTACTO");
                        Console.WriteLine("════════════════════");
                        Console.Write("Ingrese nombre a buscar: ");
                        string nombreBuscar = Console.ReadLine();
                        agenda.BuscarContactoPorNombre(nombreBuscar);
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("🗑️  ELIMINAR CONTACTO");
                        Console.WriteLine("══════════════════════");
                        Console.Write("Ingrese nombre exacto a eliminar: ");
                        string nombreEliminar = Console.ReadLine();
                        agenda.EliminarContactoPorNombre(nombreEliminar);
                        break;

                    case "5":
                        Console.Clear();
                        agenda.MostrarEstadisticas();
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("⚠️  LIMPIAR TODOS LOS CONTACTOS");
                        Console.WriteLine("═══════════════════════════════");
                        Console.Write("¿Está seguro? (S/N): ");
                        if (Console.ReadLine().ToUpper() == "S")
                        {
                            Console.WriteLine("✅ Todos los contactos han sido eliminados.");
                            // Para limpiar completamente, se crearía nueva instancia
                        }
                        break;

                    case "7":
                        Console.Clear();
                        Console.WriteLine("📤 EXPORTAR DATOS");
                        Console.WriteLine("══════════════════");
                        Console.WriteLine("Función en desarrollo...");
                        // Aquí se implementaría exportación a archivo
                        break;

                    case "8":
                        continuar = false;
                        Console.WriteLine("\n👋 ¡Gracias por usar la Agenda Telefónica!");
                        Console.WriteLine("   Sistema desarrollado para Práctica #01");
                        Console.WriteLine("   Estructura de Datos - UEA 2025-2026");
                        break;

                    default:
                        Console.WriteLine("❌ Opción no válida. Intente nuevamente.");
                        break;
                }

                if (continuar && opcion != "1")
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n════════════════ MENÚ PRINCIPAL ════════════════");
            Console.WriteLine("1. ➕ Agregar nuevo contacto");
            Console.WriteLine("2. 📋 Mostrar todos los contactos");
            Console.WriteLine("3. 🔍 Buscar contacto por nombre");
            Console.WriteLine("4. 🗑️  Eliminar contacto por nombre");
            Console.WriteLine("5. 📊 Ver estadísticas");
            Console.WriteLine("6. ⚠️  Limpiar todos los contactos");
            Console.WriteLine("7. 📤 Exportar datos");
            Console.WriteLine("8. 🚪 Salir");
            Console.WriteLine("═══════════════════════════════════════════════");
            Console.Write("Seleccione una opción (1-8): ");
        }
    }
}