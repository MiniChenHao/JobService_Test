using System.Collections.Generic;
using System.Web.Http;

namespace JobService_Test.Controllers
{
    /// <summary>
    /// VALUES 控制器
    /// </summary>
    public class ValuesController : ApiController
    {
        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="value">Value</param>
        public string Post([FromBody]string value)
        {
            return "value";
        }

        /// <summary>
        /// PUT
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="value">Value</param>
        public string Put(int id, [FromBody]string value)
        {
            return "value";
        }

        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id">ID</param>
        public string Delete(int id)
        {
            return "value";
        }
    }
}
