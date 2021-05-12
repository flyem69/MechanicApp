using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MechanicApp.Models {
    public class FirebaseAdminSDK {
        public static void Initialize() {
            FirebaseApp.Create(new AppOptions() {
                Credential = GoogleCredential.GetApplicationDefault(),
            });
        }
    }
}