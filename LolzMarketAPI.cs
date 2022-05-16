/*
    Lolz Market API by @res_surection
    https://lolz.guru/members/304338/
    Last changes 15.05.2022
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web;

namespace LolzMarketAPI
{
    public static class LolzMarketAPI
    {
        static string baseUrl = "https://api.lolz.guru/";
        private static readonly HttpClient client = new HttpClient();


        private static string myToken;
        private static int myUserId = -1;

        public static void setData(string token, int userId)
        {
            myToken = token;
            myUserId = userId;
            client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", myToken));
        }

        public static async Task<string> Get_request(string url)
        {
            var getResponse = await client.GetStringAsync(url);
            return getResponse.ToString();
        }

        public static async Task<string> Delete_request(string url)
        {
            var deleteResponse = await client.DeleteAsync(url);
            return deleteResponse.ToString();
        }

        public static async Task<string> Put_request(string url, Dictionary<string, string> values = null)
        {
            FormUrlEncodedContent content = null;
            if (values != null) { content = new FormUrlEncodedContent(values);  }
            var response = await client.PutAsync(url, content);
            string putResponse = await response.Content.ReadAsStringAsync();
            return putResponse.ToString();
        }

        public static async Task<string> Post_request(string url, Dictionary<string, string> values = null)
        {
            FormUrlEncodedContent content = null;
            if (values != null) { content = new FormUrlEncodedContent(values); }
            var response = await client.PostAsync(url, content);
            string postResponse = await response.Content.ReadAsStringAsync();
            return postResponse.ToString();
        }

        public static async Task<string> Me()
        {
            return await Get_request(String.Format("{0}market/me", baseUrl));
        }

        public static async Task<string> List(string category = null, int pmin = -1, int pmax = -1, string title = null, bool parse_sticky_items = false, Dictionary<string, string> optional_args = null)
        {
            // Displays a list of latest accounts without category parameter
            if (category == null) { return await Get_request(String.Format("{0}market", baseUrl)); }
            // Displays a list of accounts in a specific category according to your parameters
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (pmin != -1) { query["pmin"] = pmin.ToString(); }
            if (pmax != -1) { query["pmax"] = pmax.ToString(); }
            if (title != null) { query["title"] = title; }
            if (parse_sticky_items) { query["parse_sticky_items"] = parse_sticky_items.ToString(); }
            if (optional_args != null)
            {
                foreach (KeyValuePair<string, string> ex in optional_args)
                {
                    query[String.Format("{0}=", ex.Key)] = ex.Value.ToString();
                }
            }
            string resultQuery = query.ToString();
            string response = await Get_request(String.Format("{0}market/{1}?{2}", baseUrl, category, resultQuery));
            return response;
        }

        public static async Task<string> Items(int category = -1, int pmin = -1, int pmax = -1, string title = null, bool parse_sticky_items = false, Dictionary<string, string> optional_args = null)
        {
            // Displays a list of owned accounts
            if (category == -1) { return await Get_request(String.Format("{0}market/user/{1}/items", baseUrl, myUserId)); }
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (pmin != -1) { query["pmin"] = pmin.ToString(); }
            if (pmax != -1) { query["pmax"] = pmax.ToString(); }
            if (title != null) { query["title"] = title; }
            if (parse_sticky_items) { query["parse_sticky_items"] = parse_sticky_items.ToString(); }
            if (optional_args != null)
            {
                foreach (KeyValuePair<string, string> ex in optional_args)
                {
                    query[String.Format("{0}=", ex.Key)] = ex.Value.ToString();
                }
            }
            string resultQuery = query.ToString();
            string response = await Get_request(String.Format("{0}market/user/{1}/items/?category_id={2}?{3}", baseUrl, myUserId, category, resultQuery));
            return response;
        }

        public static async Task<string> Orders(int category = -1, int pmin = -1, int pmax = -1, string title = null, bool parse_sticky_items = false, Dictionary<string, string> optional_args = null)
        {
            // Displays a list of purchased accounts
            if (category == -1) { return await Get_request(String.Format("{0}market/user/{1}/orders", baseUrl, myUserId)); }
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (pmin != -1) { query["pmin"] = pmin.ToString(); }
            if (pmax != -1) { query["pmax"] = pmax.ToString(); }
            if (title != null) { query["title"] = title; }
            if (parse_sticky_items) { query["parse_sticky_items"] = parse_sticky_items.ToString(); }
            if (optional_args != null)
            {
                foreach (KeyValuePair<string, string> ex in optional_args)
                {
                    query[String.Format("{0}=", ex.Key)] = ex.Value.ToString();
                }
            }
            string resultQuery = query.ToString();
            string response = await Get_request(String.Format("{0}market/user/{1}/orders/?category_id={2}?{3}", baseUrl, myUserId, category, resultQuery));
            return response;
        }

        public static async Task<string> Fave()
        {
            // Displays a list of favourites accounts
            return await Get_request(String.Format("{0}market/fave", baseUrl));
        }

        public static async Task<string> Viewed()
        {
            // Displays a list of viewed accounts
            return await Get_request(String.Format("{0}market/viewed", baseUrl));
        }

        public static async Task<string> Item(int itemId)
        {
            // Displays account information
            return await Get_request(String.Format("{0}market/{1}", baseUrl, itemId));
        }

        /*
        ACCOUNT PURCHASING SECTION STARTS
         You need to make 3 requests: POST /market/:itemId/reserve, POST /market/:itemId/check-account and POST /market/:itemId/confirm-buy
         */
        public static async Task<string> Reserve(int itemId, int price)
        {
            // Reserves account for you. Reserve time - 300 seconds.
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["price"] = price.ToString();
            string resultQuery = query.ToString();
            return await Post_request(String.Format("{0}market/{1}/reserve?{2}", baseUrl, itemId, resultQuery));
        }

        public static async Task<string> Cancel_Reserve(int itemId)
        {
            // Cancels reserve.
            return await Post_request(String.Format("{0}market/{1}/cancel-reserve", baseUrl, itemId));
        }

        public static async Task<string> Check_Account(int itemId)
        {
            // Checking account for validity. If the account is invalid, the purchase will be canceled automatically
            // (you don't need to make request POST /market/:itemId/cancel-reserve)
            return await Post_request(String.Format("{0}market/{1}/check-account", baseUrl, itemId));
        }

        public static async Task<string> Confirm_Buy(int itemId)
        {
            // Confirms buy.
            return await Post_request(String.Format("{0}market/{1}/confirm-buy", baseUrl, itemId));
        }
        /* ACCOUNT PURCHASING SECTION ENDS */

        public static async Task<string> Transfer_Money(int receiverId, string receiverUsername, int amount,
                                                        string secret_answer, string currency = "rub", string comment = null,
                                                        bool transfer_hold = false, int hold_length_value = -1, string hold_length_option = null)
        {
            /*
             Send money to any user.

             Parameters:

                user_id             (required) User id of receiver. If user_id specified, username is not required.
                username            (required) Username of receiver. If username specified, user_id is not required.
                amount              (required) Amount to send in your currency.
                currency            (required) Using currency for amount. Allowed values: cny usd rub eur uah kzt byn gbp
                secret_answer       (required) Secret answer of your account
                comment             (optional) Transfer comment. Maximum 255 characters
                transfer_hold       (optional) (Boolean) Hold transfer or not.
                hold_length_value   (optional) Hold length value (number).
                hold_length_option  (optional) Hold length option (string). Allowed values: hour day week month year

             Hold parameters examples
                E.g. you want to hold money transfer on 3 days. hold_length_value - will be '3', hold_length_option - will be 'days'.

                E.g. you want to hold money transfer on 12 hours. hold_length_value - will be '12', hold_length_option - will be 'hours'
             */
            var values = new Dictionary<string, string> { };
            values.Add("user_id", receiverId.ToString());
            values.Add("username", receiverUsername);
            values.Add("amount", amount.ToString());
            values.Add("currency", currency);
            values.Add("secret_answer", secret_answer);
            if (comment != null) { values.Add("comment", comment); }
            if (transfer_hold) { values.Add("transfer_hold", transfer_hold.ToString()); }
            if (hold_length_value != -1) { values.Add("hold_length_value", hold_length_value.ToString()); }
            if (hold_length_option != null) { values.Add("hold_length_option", hold_length_option.ToString()); }
            return await Post_request(String.Format("{0}market/balance/transfer/", baseUrl), values);
        }

        public static async Task<string> Payments(string type = null, int pmin = -1, int pmax = -1,
                                                  string receiver = null, string sender = null,
                                                  string startDate = null, string endDate = null,
                                                  string wallet = null, string comment = null,
                                                  int is_hold = -1)
        {
            /*
             Displays list of your payments

            type        (optional): Type of operation. Allowed operation types: income cost refilled_balance 
                                                                                withdrawal_balance paid_item 
                                                                                sold_item money_transfer 
                                                                                receiving_money internal_purchase 
                                                                                claim_hold
            pmin        (optional): Minimal price of operation (Inclusive)
            pmax        (optional): Maximum price of operation (Inclusive)
            receiver    (optional): Username of user, which receive money from you
            sender      (optional): Username of user, which sent money to you
            startDate   (optional): Start date of operation (RFC 3339 date format)
            endDate     (optional): End date of operation (RFC 3339 date format)
            wallet      (optional): Wallet, which used for money payots
            comment     (optional): Comment for money transfers
            is_hold     (optional): Display hold operations 0/1 | 0 - false | 1 - true
             */
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (type != null) { query["type"] = type; }
            if (pmin != -1) { query["pmin"] = pmin.ToString(); }
            if (pmax != -1) { query["pmax"] = pmax.ToString(); }
            if (receiver != null) { query["receiver"] = receiver; }
            if (sender != null) { query["sender"] = sender; }
            if (startDate != null) { query["startDate"] = startDate; }
            if (endDate != null) { query["endDate"] = endDate; }
            if (wallet != null) { query["wallet"] = wallet; }
            if (comment != null) { query["comment"] = comment; }
            if (is_hold != -1) { query["is_hold"] = is_hold.ToString(); }
            string resultQuery = query.ToString();
            return await Get_request(String.Format("{0}market/user/{1}/payments?{2}", baseUrl, myUserId, resultQuery));
        }

        public static async Task<string> Add_Proxy(string proxyIp = null, int proxyPort = -1, string proxyUser = null, string proxyPass = null, string proxyRow = null)
        {
            // Add single proxy or proxy list
            var values = new Dictionary<string, string> { };
            if (proxyIp != null) { values.Add("proxy_ip", proxyIp); }
            if (proxyPort != -1) { values.Add("proxy_port", proxyPort.ToString()); }
            if (proxyUser != null) { values.Add("proxy_user", proxyUser); }
            if (proxyPass != null) { values.Add("proxy_pass", proxyPass); }
            if (proxyRow != null) { values.Add("proxy_row", proxyRow); }
            return await Post_request(String.Format("{0}market/proxy", baseUrl), values);
        }

        public static async Task<string> Delete_Proxy(int proxy_id = -1, bool? delete_all = null)
        {
            // Delete single or all proxies
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (proxy_id != -1) { query["proxy_id"] = proxy_id.ToString(); }
            if (delete_all != null) { query["delete_all"] = delete_all.ToString(); }
            string resultQuery = query.ToString();
            return await Delete_request(String.Format("{0}market/proxy?{1}", baseUrl, resultQuery));
        }

        public static async Task<string> Get_Proxy()
        {
            // Gets your proxy list
            return await Get_request(String.Format("{0}market/proxy", baseUrl));
        }

        public static async Task<string> Change_Settings(bool? disable_steam_guard = null, int user_allow_ask_discount = -1,
                                                         uint? max_discount_percent = null, string allow_accept_accounts = null,
                                                         bool? hide_favourites = null)
        {
            // Change settings about your profile on the market /market/me
            var values = new Dictionary<string, string> { };
            if (disable_steam_guard != null) { values.Add("disable_steam_guard", disable_steam_guard.ToString()); }
            if (user_allow_ask_discount != -1) { values.Add("user_allow_ask_discount", user_allow_ask_discount.ToString()); }
            if (max_discount_percent != null) { values.Add("max_discount_percent", max_discount_percent.ToString()); }
            if (allow_accept_accounts != null) { values.Add("allow_accept_accounts", allow_accept_accounts); }
            if (hide_favourites != null) { values.Add("hide_favourites", hide_favourites.ToString()); }
            return await Put_request(String.Format("{0}market/me", baseUrl), values);
        }

        public static async Task<string> Unstick(int itemId)
        {
            // Unstick account of the top of search
            return await Delete_request(String.Format("{0}market/{1}/stick/", baseUrl, itemId));
        }

        public static async Task<string> Stick(int itemId)
        {
            // Stick account of the top of search
            return await Post_request(String.Format("{0}market/{1}/stick/", baseUrl, itemId));
        }

        public static async Task<string> Unstar(int itemId)
        {
            // Deletes account from favourites
            return await Delete_request(String.Format("{0}market/{1}/star/", baseUrl, itemId));
        }

        public static async Task<string> Star(int itemId)
        {
            // Adds account to the favourites
            return await Post_request(String.Format("{0}market/{1}/star/", baseUrl, itemId));
        }

        public static async Task<string> Bump(int itemId)
        {
            // Bumps account in the search
            return await Post_request(String.Format("{0}market/{1}/bump/", baseUrl, itemId));
        }

        public static async Task<string> Delete_Tag(int itemId, int tagId)
        {
            // Deletes tag for the account
            return await Delete_request(String.Format("{0}market/{1}/tag?tag_id={2}", baseUrl, itemId, tagId));
        }

        public static async Task<string> Add_Tag(int itemId, int tagId)
        {
            // Adds tag for the account
            return await Post_request(String.Format("{0}market/{1}/tag?tag_id={2}", baseUrl, itemId, tagId));
        }

        public static async Task<string> Delete_Item(int itemId, string reason)
        {
            // Deletes your account from public search. Deletion type is soft. You can restore account after deletion if you want.
            return await Delete_request(String.Format("{0}market/{1}?reason={2}", baseUrl, itemId, reason));
        }

        public static async Task<string> Edit_Item(int itemId, string key = null, string value = null,
                                                   Dictionary<string, string> key_values = null, string currency = "rub")
        {
            /*
             Key list:
                title                   (optional) Russian title of account. If title specified and title_en is empty, title_en will be automatically translated to English language.
                title_en                (optional) English title of account. If title_en specified and title is empty, title will be automatically translated to Russian language.
                price                   (optional) Account price in your currency. Allowed values: cny usd rub eur uah kzt byn gbp
                item_origin             (optional) Item origin
                description             (optional) Account public description
                information             (optional) Account private information (visible for buyer only if purchased)
                has_email_login_data    (optional) Set boolean, if you have email login data
                email_login_data        (optional) Email login data (login:pass format)
                email_type              (optional) Email type. Allowed values: native, autoreg
                allow_ask_discount      (optional) Allow users to ask discount for this account
                proxy_id (optional)     Using proxy id for account checking. See GET /account/market to get or edit proxy list
             */
            var values = new Dictionary<string, string> { };
            if (key != null) { values.Add("key", key); }
            if (value != null) { values.Add("value", value); }
            values.Add("currency", currency);
            string query = string.Empty;
            if (key_values != null)
            {
                foreach (KeyValuePair<string, string> ex in key_values)
                {
                    query = query + String.Format("key_values[{0}]={1}&", ex.Key, ex.Value);
                }
            }
            return await Put_request(String.Format("{0}market/{1}/edit?{2}", baseUrl, itemId, query), values);
        }

        public static async Task<string> Add_Item(string title, int price, int category_id, string item_origin,
                                                  int extended_guarantee, string currency = "rub", string title_en = null,
                                                  string description = null, string information = null, bool? has_email_login_data = null,
                                                  string email_login_data = null, string email_type = null, bool? allow_ask_discount = null,
                                                  int proxy_id = -1)
        {
            // You need to make 2 requests: POST /market/item/add and POST /market/:itemId/goods/check For categories,
            // which required temporary email (Steam, Social Club) you need to make GET /market/:itemId/goods/add to get temporary email
            // Adds account on the market. After this request an account will have item_state = awaiting (not displayed in search)
            var values = new Dictionary<string, string> { };
            values.Add("title", title);
            values.Add("price", price.ToString());
            values.Add("category_id", category_id.ToString());
            values.Add("item_origin", item_origin);
            values.Add("extended_guarantee", extended_guarantee.ToString());
            values.Add("currency", currency);
            if (title_en != null) { values.Add("title_en", title_en); }
            if (description != null) { values.Add("description", description); }
            if (information != null) { values.Add("information", information); }
            if (has_email_login_data != null) { values.Add("has_email_login_data", has_email_login_data.ToString()); }
            if (email_login_data != null) { values.Add("email_login_data", email_login_data); }
            if (email_type != null) { values.Add("email_type", email_type); }
            if (allow_ask_discount != null) { values.Add("allow_ask_discount", allow_ask_discount.ToString()); }
            if (proxy_id != -1) { values.Add("proxy_id", proxy_id.ToString()); }
            return await Post_request(String.Format("{0}market/item/add/", baseUrl), values);
        }

        public static async Task<string> Goods_Check(int itemId, string login = null, string password = null, string login_password = null,
                                                     bool? close_item = null, Dictionary<string, string> extra = null, int resell_item_id = -1)
        {
            var values = new Dictionary<string, string> { };
            if (login != null) { values.Add("login", login); }
            if (password != null) { values.Add("password", password); }
            if (login_password != null) { values.Add("login_password", login_password); }
            if (close_item != null) { values.Add("close_item", close_item.ToString()); }
            if (resell_item_id != -1) { values.Add("resell_item_id", resell_item_id.ToString()); }
            string query = string.Empty;
            if (extra != null) 
            {
                foreach (KeyValuePair<string, string> ex in extra)
                {
                    query = query + String.Format("extra[{0}]={1}&", ex.Key, ex.Value);
                }
            }

            return await Post_request(String.Format("{0}market/{1}/goods/check?{2}", baseUrl, itemId, query), values);
        }

        public static async Task<string> Goods_Add(int itemId, int resell_item_id = -1)
        {
            // Display category list
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (resell_item_id != -1) { query["resell_item_id"] = resell_item_id.ToString(); }
            string resultQuery = query.ToString();
            return await Get_request(String.Format("{0}market/{1}/goods/add?{2}", baseUrl, itemId, resultQuery));
        }

        public static async Task<string> Category(bool? top_queries = null)
        {
            // Display category list
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (top_queries != null) { query["top_queries"] = top_queries.ToString(); }
            string resultQuery = query.ToString();
            return await Get_request(String.Format("{0}market/category?{1}", baseUrl, resultQuery));
        }

        public static async Task<string> Email_Code(int itemId, string email)
        {
            // Gets confirmation code or link.
            return await Get_request(String.Format("{0}market/{1}/email-code?email={2}", baseUrl, itemId, email));
        }

        public static async Task<string> Refuse_Guarantee(int itemId)
        {
            // Cancel guarantee of account. It can be useful for account reselling.
            return await Post_request(String.Format("{0}market/{1}/refuse-guarantee", baseUrl, itemId));
        }

        public static async Task<string> Change_Password(int itemId, bool? _cancel = null)
        {
            // Changes password of account.
            var values = new Dictionary<string, string> { };
            if (_cancel != null) { values.Add("_cancel", _cancel.ToString()); }
            return await Post_request(String.Format("{0}market/{1}/change-password", baseUrl, itemId), values);
        }

        public static async Task<string> Change_Owner(int itemId, string username, string secret_answer)
        {
            // Changes password of account.
            var values = new Dictionary<string, string> { };
            values.Add("secret_answer", secret_answer);
            values.Add("username", username);
            return await Post_request(String.Format("{0}market/{1}/change-owner", baseUrl, itemId), values);
        }
    }
}
