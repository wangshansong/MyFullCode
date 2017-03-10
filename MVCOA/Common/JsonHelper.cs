using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class JsonHelper<T> where T : class
    {
        /// <summary>
        /// 将一个DataTable对象转换为Json字符串
        /// </summary>
        /// <param name="dt">DataTable对象</param>
        /// <param name="dtName">表名</param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt, string dtName)
        {

            if (dt == null)
                throw new ArgumentNullException();

            string tableName = (dtName == string.Empty) ? "Info" : dtName;

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                JsonSerializer ser = new JsonSerializer();
                jw.WriteStartObject();
                jw.WritePropertyName(tableName);
                jw.WriteStartArray();
                foreach (DataRow dr in dt.Rows)
                {
                    jw.WriteStartObject();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        jw.WritePropertyName(dc.ColumnName);
                        ser.Serialize(jw, dr[dc].ToString());
                    }
                    jw.WriteEndObject();
                }
                jw.WriteEndArray();
                jw.WriteEndObject();
                sw.Close();
                jw.Close();
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将json字符串转换为相应的对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>返回指定类型的对象</returns>
        public static T JsonStringToModel(string jsonStr)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings();
            jss.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
            jss.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            jss.DateParseHandling = DateParseHandling.DateTime;
            // JsonConvert.DeserializeObject(jsonStr,typeof(T),
            object obj = JsonConvert.DeserializeObject(jsonStr, typeof(T), jss);
            return obj is T ? (T)obj : default(T);
        }
        /// <summary>
        /// 将json字符串转化为相应的List对象
        /// </summary>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>返回指定类型的List对象</returns>
        public static List<T> JsonStringToList(string jsonStr)
        {
            object obj = JsonConvert.DeserializeObject(jsonStr, typeof(List<T>));
            return obj as List<T>;
        }

        /// <summary>
        /// 将List转换为Json字符串
        /// </summary>
        /// <param name="list">待转换的List对象</param>
        /// <returns>Json字符串</returns>
        public static string ListToJsonString(List<T> list)
        {
            if (list == null)
                throw new ArgumentNullException();
            //  string str = new JavaScriptSerializer().Serialize(list); 
            IsoDateTimeConverter timeConverter = new IsoDateTimeConverter();
            timeConverter.DateTimeFormat = "yyyy-MM-dd";
            JsonSerializerSettings setting = new JsonSerializerSettings();
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            setting.Converters.Add(timeConverter);
            //string str = JsonConvert.SerializeObject(list);
            string str = JsonConvert.SerializeObject(list, Formatting.Indented, setting);

            return str;
        }

        /// <summary>
        /// 将实体Model转换为Json字符串
        /// </summary>
        /// <param name="obj">待转换的实体Model对象</param>
        /// <returns>Json字符串</returns>
        public static string ModelToJsonString(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException();
            string str = JsonConvert.SerializeObject(obj);
            return str;
        }
    }
}
