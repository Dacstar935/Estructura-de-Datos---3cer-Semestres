using System;
using System.Collections.Generic;
using System.Linq;

namespace TraductorEspanolIngles
{
    class Program
    {
        // Diccionario principal que almacena las palabras en español y su traducción al inglés
        // StringComparer.OrdinalIgnoreCase permite que las búsquedas no distingan entre mayúsculas y minúsculas
        static Dictionary<string, string> diccionario = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        static void Main(string[] args)
        {
            // Llamamos al método que carga las palabras iniciales
            InicializarDiccionario();
            
            int opcion; // Variable para guardar la opción del menú

            // Bucle do-while para que el menú se repita hasta que el usuario elija salir (opción 0)
            do
            {
                MostrarMenu(); // Mostramos el menú
                string input = Console.ReadLine(); // Leemos la entrada del usuario
                
                // Intentamos convertir la entrada a número. Si no es válida, mostramos error
                if (!int.TryParse(input, out opcion))
                {
                    Console.WriteLine("\nOpción no válida. Debe ingresar un número.\n");
                    continue; // Volvemos al inicio del bucle
                }

                // Según la opción elegida, llamamos a un método u otro
                switch (opcion)
                {
                    case 1:
                        TraducirFrase(); // Opción para traducir
                        break;
                    case 2:
                        AgregarPalabras(); // Opción para agregar palabras
                        break;
                    case 0:
                        Console.WriteLine("\n¡Hasta luego!\n"); // Mensaje de despedida
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Intente de nuevo.\n"); // Opción fuera de rango
                        break;
                }

            } while (opcion != 0); // Repetimos mientras no se elija salir
        }

        // MÉTODO: Mostrar menú principal
        static void MostrarMenu()
        {
            Console.WriteLine("==================== MENÚ ====================");
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.WriteLine("===============================================");
            Console.Write("Seleccione una opción: ");
        }

        // MÉTODO: Inicializar diccionario con palabras base
        // Español -> Inglés usando la lista proporcionada
        static void InicializarDiccionario()
        {
            // Diccionario temporal con las palabras en español y su traducción al inglés
            // Se incluyen las variantes como "camino/forma", "niño/niña", "punto/tema", "empresa/compañía"
            var palabrasBase = new Dictionary<string, string>
            {
                // Lista original: Time — tiempo
                {"tiempo", "time"},
                
                // Lista original: Person — persona
                {"persona", "person"},
                
                // Lista original: Year — año
                {"año", "year"},
                
                // Lista original: Way — camino / forma
                {"camino", "way"},
                {"forma", "way"},      // Segunda opción para "way"
                
                // Lista original: Day — día
                {"día", "day"},
                
                // Lista original: Thing — cosa
                {"cosa", "thing"},
                
                // Lista original: Man — hombre
                {"hombre", "man"},
                
                // Lista original: World — mundo
                {"mundo", "world"},
                
                // Lista original: Life — vida
                {"vida", "life"},
                
                // Lista original: Hand — mano
                {"mano", "hand"},
                
                // Lista original: Part — parte
                {"parte", "part"},
                
                // Lista original: Child — niño/a
                {"niño", "child"},
                {"niña", "child"},     // Para "child" como niña
                
                // Lista original: Eye — ojo
                {"ojo", "eye"},
                
                // Lista original: Woman — mujer
                {"mujer", "woman"},
                
                // Lista original: Place — lugar
                {"lugar", "place"},
                
                // Lista original: Work — trabajo
                {"trabajo", "work"},
                
                // Lista original: Week — semana
                {"semana", "week"},
                
                // Lista original: Case — caso
                {"caso", "case"},
                
                // Lista original: Point — punto / tema
                {"punto", "point"},
                {"tema", "point"},     // Segunda opción para "point"
                
                // Lista original: Government — gobierno
                {"gobierno", "government"},
                
                // Lista original: Company — empresa / compañía
                {"empresa", "company"},
                {"compañía", "company"} // Segunda opción para "company"
            };

            // Recorremos el diccionario temporal y agregamos cada palabra al diccionario principal
            foreach (var palabra in palabrasBase)
            {
                // Usamos la palabra en español como clave y su traducción al inglés como valor
                diccionario[palabra.Key] = palabra.Value;
            }

            // Mostramos cuántas palabras se cargaron
            Console.WriteLine($"Diccionario español -> inglés inicializado con {diccionario.Count} palabras.\n");
        }

        // MÉTODO: Traducir una frase completa
        static void TraducirFrase()
        {
            Console.Write("\nIngrese la frase a traducir: ");
            string frase = Console.ReadLine(); // Leemos la frase

            // Validamos que la frase no esté vacía
            if (string.IsNullOrWhiteSpace(frase))
            {
                Console.WriteLine("No ingresó ninguna frase.\n");
                return; // Salimos del método
            }

            // Definimos los caracteres que separan las palabras
            // Incluimos espacios, signos de puntuación, etc.
            char[] separadores = new char[] { ' ', ',', '.', ';', ':', '!', '¡', '?', '¿', '-', '(', ')' };
            
            // Dividimos la frase en palabras usando los separadores
            // StringSplitOptions.RemoveEmptyEntries elimina elementos vacíos
            string[] palabras = frase.Split(separadores, StringSplitOptions.RemoveEmptyEntries);

            // Lista para guardar las palabras ya traducidas
            List<string> palabrasTraducidas = new List<string>();

            // Recorremos cada palabra de la frase original
            foreach (string palabra in palabras)
            {
                // Verificamos si la palabra existe en el diccionario (español -> inglés)
                // ContainsKey busca sin importar mayúsculas/minúsculas gracias a StringComparer.OrdinalIgnoreCase
                if (diccionario.ContainsKey(palabra))
                {
                    // Si existe, agregamos su traducción al inglés
                    palabrasTraducidas.Add(diccionario[palabra]);
                }
                else
                {
                    // Si no existe, conservamos la palabra original en español
                    palabrasTraducidas.Add(palabra);
                }
            }

            // Llamamos a un método especial para reconstruir la frase con los signos originales
            string fraseTraducida = ReconstruirFrase(frase, palabras, palabrasTraducidas);

            // Mostramos el resultado
            Console.WriteLine($"\nTraducción: {fraseTraducida}\n");
        }

        // MÉTODO: Reconstruir frase respetando signos originales
        static string ReconstruirFrase(string fraseOriginal, string[] palabrasOriginales, List<string> palabrasTraducidas)
        {
            string fraseReconstruida = fraseOriginal; // Empezamos con la frase original
            
            // Recorremos cada palabra que fue procesada
            for (int i = 0; i < palabrasOriginales.Length; i++)
            {
                // Solo reemplazamos si la palabra cambió (es decir, si se tradujo)
                if (palabrasOriginales[i] != palabrasTraducidas[i])
                {
                    // Buscamos la posición de la palabra original en la frase
                    // Usamos IndexOf con StringComparison.OrdinalIgnoreCase para ignorar mayúsculas
                    int pos = fraseReconstruida.IndexOf(palabrasOriginales[i], StringComparison.OrdinalIgnoreCase);
                    
                    if (pos >= 0) // Si encontramos la palabra
                    {
                        // Llamamos a un método que respeta si la palabra original tenía mayúsculas
                        string reemplazo = RespetarMayusculas(palabrasOriginales[i], palabrasTraducidas[i]);
                        
                        // Extraemos la parte antes de la palabra
                        string antes = fraseReconstruida.Substring(0, pos);
                        // Extraemos la parte después de la palabra
                        string despues = fraseReconstruida.Substring(pos + palabrasOriginales[i].Length);
                        
                        // Reconstruimos la frase con la palabra traducida
                        fraseReconstruida = antes + reemplazo + despues;
                    }
                }
            }
            
            return fraseReconstruida;
        }

// MÉTODO: Respetar mayúsculas al traducir
        static string RespetarMayusculas(string original, string traducida)
        {
            if (string.IsNullOrEmpty(original)) return traducida;

            // Caso 1: La palabra original está completamente en MAYÚSCULAS
            if (original.All(char.IsUpper))
            {
                return traducida.ToUpper(); // Devolvemos la traducción en mayúsculas
            }
            // Caso 2: La primera letra es mayúscula (Ej: "Hola")
            else if (char.IsUpper(original[0]))
            {
                // Convertimos la primera letra de la traducción a mayúscula y el resto a minúscula
                return char.ToUpper(traducida[0]) + traducida.Substring(1).ToLower();
            }
            // Caso 3: Todo en minúsculas
            else
            {
                return traducida.ToLower(); // Devolvemos la traducción en minúsculas
            }
        }

        // MÉTODO: Agregar nuevas palabras al diccionario
        static void AgregarPalabras()
        {
            Console.WriteLine("\nAgregar nuevas palabras al diccionario");
            Console.WriteLine("(Escriba 'salir' en cualquier campo para terminar)\n");

            // Bucle infinito que se rompe cuando el usuario escribe "salir"
            while (true)
            {
                Console.Write("Palabra en ESPAÑOL: ");
                string español = Console.ReadLine()?.Trim(); // Leemos y quitamos espacios al inicio/final

                // Si el usuario escribe "salir" o deja vacío, salimos del bucle
                if (string.IsNullOrWhiteSpace(español) || español.ToLower() == "salir")
                    break;

                Console.Write("Traducción al INGLÉS: ");
                string ingles = Console.ReadLine()?.Trim();

                // Si el usuario escribe "salir" o deja vacío, salimos del bucle
                if (string.IsNullOrWhiteSpace(ingles) || ingles.ToLower() == "salir")
                    break;

                // Verificamos si la palabra en español ya existe en el diccionario
                if (diccionario.ContainsKey(español))
                {
                    // Informamos al usuario que ya existe y mostramos la traducción actual
                    Console.WriteLine($"La palabra '{español}' ya existe en el diccionario con traducción: '{diccionario[español]}'");
                    Console.Write("¿Desea sobrescribirla? (s/n): ");
                    string respuesta = Console.ReadLine()?.Trim().ToLower();
                    
                    // Si el usuario confirma, actualizamos la palabra
                    if (respuesta == "s" || respuesta == "si" || respuesta == "sí")
                    {
                        diccionario[español] = ingles; // Sobrescribimos
                        Console.WriteLine($"Palabra '{español}' actualizada correctamente.\n");
                    }
                    else
                    {
                        Console.WriteLine("No se realizaron cambios.\n");
                    }
                }
                else
                {
                    // Si no existe, simplemente la agregamos
                    diccionario.Add(español, ingles);
                    Console.WriteLine($"Palabra '{español}' agregada correctamente.\n");
                }

                Console.WriteLine("--- Siguiente palabra ---\n");
            }

            // Mostramos el total actualizado de palabras en el diccionario
            Console.WriteLine($"Diccionario actualizado. Total: {diccionario.Count} palabras.\n");
        }
    }
}