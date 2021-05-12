using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace MechanicApp.App_Start {
    public class FirebaseAdminSDKInit {
        public static void Initialization() {
            FirebaseApp.Create(new AppOptions() {
                Credential = GoogleCredential.GetApplicationDefault(),
            });
        }
    }
}