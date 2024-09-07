using ControlEscolar.Models.Entidades;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ControlEscolar.Models
{
    public class NMateriasAlumno
    {
        private string _urlWebAPIMateriasAlumno;

        public NMateriasAlumno()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _urlWebAPIMateriasAlumno = builder.GetSection("urlWebAPIMateriasAlumno").Value;
        }

        public async Task<List<MateriasAlumno>> Consultar()
        {
            List<MateriasAlumno> lstMateriasAlumno = new List<MateriasAlumno>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var resultTask = await client.GetAsync(_urlWebAPIMateriasAlumno);
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        lstMateriasAlumno = JsonConvert.DeserializeObject<List<MateriasAlumno>>(json);
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
            return lstMateriasAlumno;
        }
        public async Task<MateriasAlumno> Consultar(int id)
        {
            MateriasAlumno oMateriasAlumno = new MateriasAlumno();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var resultTask = await client.GetAsync($"{_urlWebAPIMateriasAlumno}/{id}");
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        oMateriasAlumno = JsonConvert.DeserializeObject<MateriasAlumno>(json);
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
            return oMateriasAlumno;
        }
        public async Task Eliminar(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var resultTask = await client.DeleteAsync($"{_urlWebAPIMateriasAlumno}/{id}");
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
        public async Task<MateriasAlumno> Agregar(MateriasAlumno oMateriasAlumno)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent oMateriasAlumnoHttp = new StringContent(JsonConvert.SerializeObject(oMateriasAlumno), Encoding.UTF8);
                    oMateriasAlumnoHttp.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var resultTask = await client.PostAsync(_urlWebAPIMateriasAlumno, oMateriasAlumnoHttp);
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        oMateriasAlumno = JsonConvert.DeserializeObject<MateriasAlumno>(json);
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
            return oMateriasAlumno;
        }
        public async Task<MateriasAlumno> Actualizar(MateriasAlumno oMateriasAlumno)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent oMateriasAlumnoHttp = new StringContent(JsonConvert.SerializeObject(oMateriasAlumno), Encoding.UTF8);
                    oMateriasAlumnoHttp.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var resultTask = await client.PutAsync($"{_urlWebAPIMateriasAlumno}/{oMateriasAlumno.Id}", oMateriasAlumnoHttp);
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        oMateriasAlumno = JsonConvert.DeserializeObject<MateriasAlumno>(json);
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
            return oMateriasAlumno;
        }
    }
}
