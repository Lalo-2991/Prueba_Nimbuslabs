using ControlEscolar.Models.Entidades;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
namespace ControlEscolar.Models
{
    public class NAlumno
    {
        private string _urlWebAPIAlumno;

        public NAlumno()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _urlWebAPIAlumno = builder.GetSection("urlWebAPIAlumno").Value;
        }

        public async Task<List<Alumno>> Consultar()
        {
            List<Alumno> lstAlumnos = new List<Alumno>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var resultTask = await client.GetAsync(_urlWebAPIAlumno);
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        lstAlumnos = JsonConvert.DeserializeObject<List<Alumno>>(json);
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
            return lstAlumnos;
        }
        public async Task<Alumno> Consultar(int id)
        {
            Alumno oAlumno = new Alumno();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var resultTask = await client.GetAsync($"{_urlWebAPIAlumno}/{id}");
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        oAlumno = JsonConvert.DeserializeObject<Alumno>(json);
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
            return oAlumno;
        }
        public async Task Eliminar(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var resultTask = await client.DeleteAsync($"{_urlWebAPIAlumno}/{id}");
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
        public async Task<Alumno> Agregar(Alumno oAlumno)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent oAlumnoHttp = new StringContent(JsonConvert.SerializeObject(oAlumno), Encoding.UTF8);
                    oAlumnoHttp.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var resultTask = await client.PostAsync(_urlWebAPIAlumno, oAlumnoHttp);
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        oAlumno = JsonConvert.DeserializeObject<Alumno>(json);
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
            return oAlumno;
        }
        public async Task<Alumno> Actualizar(Alumno oAlumno)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpContent oAlumnoHttp = new StringContent(JsonConvert.SerializeObject(oAlumno), Encoding.UTF8);
                    oAlumnoHttp.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var resultTask = await client.PutAsync($"{_urlWebAPIAlumno}/{oAlumno.Id}", oAlumnoHttp);
                    if (resultTask.IsSuccessStatusCode)
                    {
                        string json = await resultTask.Content.ReadAsStringAsync();
                        oAlumno = JsonConvert.DeserializeObject<Alumno>(json);
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
            return oAlumno;
        }
    }
}
