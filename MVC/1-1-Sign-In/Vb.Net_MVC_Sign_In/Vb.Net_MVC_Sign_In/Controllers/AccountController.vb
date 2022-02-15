Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OpenIdConnect
Imports Microsoft.Owin.Security

Public Class AccountController
    Inherits Controller
    
    Public Sub SignIn()
        ' Send an OpenID Connect sign-in request.
        If Not Request.IsAuthenticated Then
          HttpContext.GetOwinContext().Authentication.Challenge(New AuthenticationProperties() With { .RedirectUri = "/" }, 
              OpenIdConnectAuthenticationDefaults.AuthenticationType)
        End If
    End Sub

    Public Sub SignOut()
      Dim callbackUrl As String = Url.Action("SignOutCallback", "Account", routeValues := Nothing, protocol := Request.Url.Scheme)

      HttpContext.GetOwinContext().Authentication.SignOut(New AuthenticationProperties() With { .RedirectUri = callbackUrl }, 
          OpenIdConnectAuthenticationDefaults.AuthenticationType, CookieAuthenticationDefaults.AuthenticationType)
    End Sub

    Public Function SignOutCallback() As ActionResult
        If Request.IsAuthenticated Then
            ' Redirect to home page if the user is authenticated.
            Return RedirectToAction("Index", "Home")
        End If

        Return View()
    End Function
End Class
