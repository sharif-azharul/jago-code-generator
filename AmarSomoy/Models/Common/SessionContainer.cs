using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public sealed class SessionContainer
    {
        #region Constrain ------------------------------
        public SessionContainer()
        {
            //this.USER_ID = "Admin";
            //this.USER_NM = "Admin";
            SessionUtility.SessionContainer = this;
        }

        #endregion -----------------------------Constrain
        #region Properties -----------------------------
        /// <summary>
        /// Get or Set USER_ID
        /// </summary>
        public string USER_ID { get; set; }

        /// <summary>
        /// Get or Set USER_NAME
        /// </summary>
        public string USER_NM { get; set; }

        public int USER_TYPE_ID { get; set; }

        /// <summary>
        /// Get or Set OBJ_CLASS to manage session
        /// </summary>
        public object OBJ_CLASS { get; set; }

        /// <summary>
        /// Get or Set WORKING_UNIT_CODE
        /// Set after user loged In
        /// </summary>
        public string CONTRACT_NO { get; set; }

        /// <summary>
        /// Get or Set WorkingUnitId
        /// Property Assigned after login
        /// </summary>
        public string CUSTOMER_ID { get; set; }

        public string LOCATION_ID { get; set; }

        public string AGENT_ID { get; set; }

        public bool IS_PRICE_HIDE { get; set; }

        public bool IS_QUANTITY_HIDE { get; set; }

        /// <summary>
        /// Get or Set COMPANY_CODE
        /// </summary>
        public string COMPANY_CODE { get; set; }

        /// <summary>
        /// Get or Set WorkingUnitCollection
        /// </summary>
        public DataTable WORKING_UNIT_COLLECTION { get; set; }
        #endregion -------------------------------Properties

        public string MESSAGE { get; set; }

        /// <summary> Gets. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="key"> The key. </param>
        /// <returns> . </returns>
        public T GetObject<T>(string key)
        {
            object o = HttpContext.Current.Session[key];
            if (o is T)
            {
                return (T)o;
            }

            return default(T);
        }

        /// <summary> Sets. </summary>
        /// <typeparam name="T"> Generic type parameter. </typeparam>
        /// <param name="key">  The key. </param>
        /// <param name="item"> The item. </param>
        public void SetObject<T>(string key, T item)
        {
            HttpContext.Current.Session[key] = item;
        }
    }//End of Class
}