using ControlEscolar.Models.Entidades;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ControlEscolar.Models
{
    public class NMateria
    {
        private string _urlWebAPIMateria;

        public NMateria()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _urlWebAPIMateria = builder.GetSection("urlWebAPIMateria").Value;
        }

        public async Task<List<Materia>> Consultar()
        {
            List<Materia> lstMateria = new List<Materia>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var resultTask = await client.GetAsync(_urlWebAPIMateria);
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        lstMateria = JsonConvert.DeserializeObject<List<Materia>>(json);
                    }
                    else
                    {
                        throw new Exception($"{resultTask.StatusCode}");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error en recepción de información {ex.Message}");
            }
            return lstMateria;
        }
        public async Task<Materia> Consultar(int id)
        {
            Materia oMateria = new Materia();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var resultTask = await client.GetAsync($"{_urlWebAPIMateria}/{id}");
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        oMateria = JsonConvert.DeserializeObject<Materia>(json);
                    }
                    else
                    {
                        throw new Exception($"{resultTask.StatusCode}");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error en recepción de información {ex.Message}");
            }
            return oMateria;
        }
        public async Task Eliminar(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var resultTask = await client.DeleteAsync($"{_urlWebAPIMateria}/{id}");
                    if (!resultTask.IsSuccessStatusCode)
                    {
                        throw new Exception($"{resultTask.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en recepción de información {ex.Message}");
            }
        }
        public async Task<Materia> Agregar(Materia oMateria)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent oMateriaHttp = new StringContent(JsonConvert.SerializeObject(oMateria), Encoding.UTF8);
                    oMateriaHttp.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var resultTask = await client.PostAsync(_urlWebAPIMateria, oMateriaHttp);
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        oMateria = JsonConvert.DeserializeObject<Materia>(json);
                    }
                    else
                    {
                        throw new Exception($"{resultTask.StatusCode}");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error en recepción de información {ex.Message}");
            }
            return oMateria;
        }
        public async Task<Materia> Actualizar(Materia oMateria)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent oMateriaHttp = new StringContent(JsonConvert.SerializeObject(oMateria), Encoding.UTF8);
                    oMateriaHttp.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var resultTask = await client.PutAsync($"{_urlWebAPIMateria}/{oMateria.Id}", oMateriaHttp);
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        oMateria = JsonConvert.DeserializeObject<Materia>(json);
                    }
                    else
                    {
                        throw new Exception($"{resultTask.StatusCode}");
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error en recepción de información {ex.Message}");
            }
            return oMateria;
        }
    }
}
