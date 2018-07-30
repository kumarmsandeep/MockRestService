using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using MockRestService.Model;

namespace MockRestService.Utils
{
    public class WorkSpaceMgr
    {
        private WorkSpace workspace;

        public WorkSpaceMgr(WorkSpace workspace)
        {
            this.workspace = workspace;
        }

        public WorkSpaceMgr(String filePath)
        {
            
        }

        public WebRequest getTargetWebRequest(HttpListenerContext context)
        {
            TargetHost host = workspace.TargetHost;
            HttpWebRequest destination = (HttpWebRequest)WebRequest.Create(host.Protocol + "://" + host.Host + ((host.Port > 0) ? (host.Port + "") : ("")));
            HttpListenerRequest request = context.Request;

            System.Collections.Specialized.NameValueCollection headers = request.Headers;
            if (headers != null)
            {
//                destination.Headers = new WebHeaderCollection();
                foreach (String name in headers)
                {
                    if (WebHeaderCollection.IsRestricted(name))
                    {
                        continue;
                    }
                    destination.Headers.Add(name, headers[name]);
                }
            }
            
            if (request.AcceptTypes != null && request.AcceptTypes.Any())
            {
                destination.Accept = string.Join(",", request.AcceptTypes);
            }
            destination.ContentType = request.ContentType;
            destination.Referer = request.UrlReferrer != null?request.UrlReferrer.AbsoluteUri : string.Empty;
            destination.UserAgent = request.UserAgent;
            destination.Method = request.HttpMethod;
            if (destination.CookieContainer == null)
            {
                destination.CookieContainer = new CookieContainer();
            }
            foreach(Cookie cookie in request.Cookies) {
                destination.CookieContainer.Add(destination.Address, cookie);
            }            
            return destination;
        }

        public APIMock GetAPIMock(HttpListenerRequest httpListenerRequest)
        {
            Dictionary<String, String> pathParams = new Dictionary<String, String>();
            Dictionary<String, String> queryParams = new Dictionary<String, String>();

            RestAPI restApi = GetRestAPI(httpListenerRequest, out pathParams, out queryParams);

            if (restApi != null)
            {
                if (restApi.ApiMocks != null)
                {
                    foreach (APIMock apiMock in restApi.ApiMocks)
                    {
                        if (apiMock != null && apiMock.Rules != null)
                        {
                            foreach (MockRule rule in apiMock.Rules)
                            {
                                if (rule is PathRule)
                                {

                                }
                                else if (rule is HeaderRule)
                                {

                                }
                                else if (rule is QueryRule)
                                {

                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        public RestAPI GetRestAPI(HttpListenerRequest httpListenerRequest, out Dictionary<String,String> pahParams, out Dictionary<String,String> queryParams)
        {
            UriTemplateMatch templateMatch = null;
            
            RestAPI restAPI = GetRestAPI(httpListenerRequest, out templateMatch);

            if (templateMatch != null && restAPI != null)
            {

                pahParams = new Dictionary<String, String>();
                queryParams = new Dictionary<String, String>();

                if (templateMatch != null)
                {
                    if (templateMatch.BoundVariables != null)
                    {
                        foreach (String variableName in templateMatch.BoundVariables.Keys)
                        {
                            pahParams.Add(variableName, templateMatch.BoundVariables[variableName]);
                        }
                    }

                    if (templateMatch.QueryParameters != null)
                    {
                        foreach (string variableName in templateMatch.QueryParameters.Keys)
                        {
                            queryParams.Add(variableName, templateMatch.QueryParameters[variableName]);
                        }
                    }
                }
            }
            else
            {
                pahParams = queryParams = null;
            }
            return restAPI;
        }

        public RestAPI GetRestAPI(HttpListenerRequest httpListenerRequest, out UriTemplateMatch templateMatch)
        {
            String httpMethod = httpListenerRequest.HttpMethod;
            foreach (KeyValuePair<String, RestAPI> entry in workspace.Apis)
            {
                RestAPI restApi = entry.Value;
                if (restApi.Method.ToString().ToLower().Equals(httpMethod.ToLower()))
                {
                    templateMatch = getPathMatches(restApi.Path, httpListenerRequest.Url);
                    if (templateMatch != null)
                    {
                        return restApi;
                    }
                }
            }
            templateMatch = null;
            return null;
        }

        private UriTemplateMatch getPathMatches(String pathTemplate, Uri requestPath)
        {
            Uri prefix = new Uri("/", UriKind.Relative);

            UriTemplate template = new UriTemplate(pathTemplate, true);

            UriTemplateMatch results = template.Match(prefix, requestPath);
            
            return results;
        }
    }
}
