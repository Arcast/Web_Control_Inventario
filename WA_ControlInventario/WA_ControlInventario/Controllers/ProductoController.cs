using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WA_ControlInventario.Models;

namespace WA_ControlInventario.Controllers
{
    public class ProductoController : Controller
    {
        //Cambiar la url por la ruta que levanta el api
        private readonly string _url = "https://localhost:7150/Producto/";

        #region Mostrar Productos

        public async Task<ActionResult> MostrarProductos()
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var url = _url + "MostrarProductos";
                    //Petición HTTP GET a la API
                    var response = await _httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer y deserializar los datos recibidos
                        var jsonData = await response.Content.ReadAsStringAsync();
                        var productos = JsonConvert.DeserializeObject<List<ProductoDTO>>(jsonData);

                        // Pasar los datos a la vista
                        return View(productos);
                    }

                    // Manejo de error
                    return View("Error");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        #endregion

        #region Nuevo Producto

        public async Task<ActionResult> NuevoProducto(BodegaDTO bodegaDTO)
        {
            // Procesa los datos de `bodega` recibidos
            return View(new ProductoDTO());
        }

        //Envía los datos al API al guardar la Producto
        [HttpPost]
        public async Task<ActionResult> GuardarProducto(ProductoDTO producto)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var url = _url + "Guardar";

                    var jsonInput = JsonConvert.SerializeObject(producto);
                    var content = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer y deserializar los datos recibidos
                        var jsonData = await response.Content.ReadAsStringAsync();
                        var ProductoId = JsonConvert.DeserializeObject(jsonData);

                        //// Redirige a una vista de éxito
                        //return RedirectToAction("Confirmacion");

                        TempData["SuccessMessage"] = "¡Producto guardado exitosamente!";
                        return RedirectToAction("NuevoProducto");
                    }

                    // Manejo de error
                    return View("Error");

                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

        #endregion

        #region Modificar Producto

        public async Task<ActionResult> ListarProductos()
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var url = _url + "MostrarProductos";
                    //Petición HTTP GET a la API
                    var response = await _httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer y deserializar los datos recibidos
                        var jsonData = await response.Content.ReadAsStringAsync();
                        var productos = JsonConvert.DeserializeObject<List<ProductoDTO>>(jsonData);

                        // Pasar los datos a la vista
                        return View(productos);
                    }

                    // Manejo de error
                    return View("Error");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        // Muestra el formulario para editar un producto en específico
        public async Task<ActionResult> EditarProducto(Guid id)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var response = await _httpClient.GetAsync(_url + "BuscarPorId?Id=" + id.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonData = await response.Content.ReadAsStringAsync();
                        var producto = JsonConvert.DeserializeObject<ProductoDTO>(jsonData);
                        return View(producto);
                    }

                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ActualizarProducto(ProductoDTO producto)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var url = _url + "Modificar";
                    var jsonData = JsonConvert.SerializeObject(producto);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Redirige de vuelta a la lista de bodegas después de actualizar
                        return RedirectToAction("ListarProductos");
                    }

                    return View("Error");

                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        #endregion

    }
}