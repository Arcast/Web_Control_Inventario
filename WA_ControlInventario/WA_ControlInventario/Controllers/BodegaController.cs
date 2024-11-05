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
    public class BodegaController : Controller
    {
        //Cambiar la url por la ruta que levanta el api
        private readonly string _url = "https://localhost:7150/Bodega/";

        #region Guardar Bodega

        public async Task<ActionResult> NuevaBodega(BodegaDTO bodegaDTO)
        {
            // Procesa los datos de `bodega` recibidos
            return View(new BodegaDTO());
        }

        //Envía los datos al API al guardar la bodega
        [HttpPost]
        public async Task<ActionResult> GuardarBodega(BodegaDTO bodega)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var url = _url + "Guardar";

                    var jsonInput = JsonConvert.SerializeObject(bodega);
                    var content = new StringContent(jsonInput, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer y deserializar los datos recibidos
                        var jsonData = await response.Content.ReadAsStringAsync();
                        var BodegaId = JsonConvert.DeserializeObject(jsonData);

                        //// Redirige a una vista de éxito
                        //return RedirectToAction("Confirmacion");

                        TempData["SuccessMessage"] = "¡Bodega guardada exitosamente!";
                        return RedirectToAction("NuevaBodega");
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

        #region Mostrar Bodega

        // GET: Bodega
        public async Task<ActionResult> MostrarBodega()
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var url = _url + "MostrarBodegas";
                    //Petición HTTP GET a la API
                    var response = await _httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Leer y deserializar los datos recibidos
                        var jsonData = await response.Content.ReadAsStringAsync();
                        var bodegas = JsonConvert.DeserializeObject<List<BodegaDTO>>(jsonData);

                        // Pasar los datos a la vista
                        return View(bodegas);
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

        #region Modificar Bodega


        // Muestra la lista de bodegas
        public async Task<ActionResult> ListarBodegas()
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var response = await _httpClient.GetAsync(_url + "MostrarBodegas");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonData = await response.Content.ReadAsStringAsync();
                        var bodegas = JsonConvert.DeserializeObject<List<BodegaDTO>>(jsonData);
                        return View(bodegas);
                    }

                    return View("Error");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // Muestra el formulario para editar una bodega específica
        public async Task<ActionResult> EditarBodega(Guid id)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var response = await _httpClient.GetAsync(_url + "BuscarPorId?Id=" + id.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonData = await response.Content.ReadAsStringAsync();
                        var bodega = JsonConvert.DeserializeObject<BodegaDTO>(jsonData);
                        return View(bodega);
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
        public async Task<ActionResult> ActualizarBodega(BodegaDTO bodega)
        {
            try
            {
                using (var _httpClient = new HttpClient())
                {
                    var url = _url + "Modificar";
                    var jsonData = JsonConvert.SerializeObject(bodega);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Redirige de vuelta a la lista de bodegas después de actualizar
                        return RedirectToAction("ListarBodegas");
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