using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class AuthManager : MonoBehaviour
{

    //Input Fields
    public TMP_InputField emailInput;
    public TMP_InputField nameInput;
    public TMP_InputField passwordInput;


    public TextMeshProUGUI displayName;
    public TextMeshProUGUI errorMeshContent;

    //Buttons
    public GameObject signUpBtn;
    public GameObject signInBtn;
    public GameObject forgetPasswordBtn;
    public GameObject signOutBtn;

    //Setting database
    DatabaseReference dbReference;
    FirebaseAuth auth;

    private void Awake()
    {
        //initialize database on wake
        auth = FirebaseAuth.DefaultInstance;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //Getting parameters from Form Manager
    public void SignInUser()
    {
        //retrieving variable data from input fields
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (ValidateEmail(email) && ValidatePassword(password))
        {
            //signing in user
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
             {
                 if (task.IsCanceled)
                 {
                     string errorMsg = this.HandleSignInError(task);
                     errorMeshContent.text = errorMsg;
                     Debug.Log("Error in sign in" + errorMsg);
                     errorMeshContent.gameObject.SetActive(true);
                     Debug.LogError("Sorry, signing in account was canceled");
                     return;
                 }

                 else if (task.IsFaulted)
                 {
                     string errorMsg = this.HandleSignInError(task);
                     errorMeshContent.text = errorMsg;
                     Debug.Log("Error in sign in" + errorMsg);
                     errorMeshContent.gameObject.SetActive(true);
                     Debug.LogError("Sorry, signing in account was faulted");
                     return;
                 }

                 else if (task.IsCompleted)
                 {
                     errorMeshContent.gameObject.SetActive(false);
                     FirebaseUser currentUser = task.Result;
                     Debug.LogFormat("User {0} signed in successfully", currentUser.UserId);
                     SceneManager.LoadScene(1);
                     return;
                 }
             });
        }
        else
        {
            errorMeshContent.text="Error in Signing in, invalid email or password";
            errorMeshContent.gameObject.SetActive(true);
        }
    }

    public async void SignUpNewUser()
    {
        //retrieving variable data from input fields
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (ValidateEmail(email) && ValidatePassword(password))
        {
            FirebaseUser newUser = await SignUpNewUserOnly(email, password);
            if (newUser != null)
            {

                string username = nameInput.text.Trim();
                await CreateNewPlayer(newUser.UserId, username, username, newUser.Email);
                await UpdatePlayerDisplayName(username);
            }
        }

        else
        {
            errorMeshContent.text = "Error in Signing up, Invalid email or password";
            errorMeshContent.gameObject.SetActive(true);
        }
    }

    public async Task<FirebaseUser> SignUpNewUserOnly(string email, string password)
    {
        //creating user
        FirebaseUser newUser = null;
        await auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                if (task.Exception!=null)
                {
                    string errorMsg = this.HandleSignUpError(task);
                    errorMeshContent.text = errorMsg;
                    Debug.LogError("Creating account was canceled."+errorMsg);
                    errorMeshContent.gameObject.SetActive(true);
                }

                return;
            }
            else if (task.IsFaulted)
            {
                if (task.Exception != null)
                {
                    string errorMsg = this.HandleSignUpError(task);
                    errorMeshContent.text = errorMsg;
                    errorMeshContent.gameObject.SetActive(true);
                    Debug.LogError("Creating account was canceled." + errorMsg);
                    return;
                }
            }

            else if (task.IsCompleted)
            {
                errorMeshContent.gameObject.SetActive(false);
                // Firebase user has been created.
                newUser = task.Result;
                Debug.LogFormat("Account created successfully: {0} {1}", newUser.Email, newUser.UserId);
            }
        });

        return newUser;
    }


    public async Task CreateNewPlayer(string uuid, string userName, string displayName, string email)
    {
        Player newPlayer = new Player(userName, displayName, email);
        Debug.LogFormat("Player details : {0}", newPlayer.PrintPlayer());

        //root/player/$uuid
        dbReference.Child("players/" + uuid).SetRawJsonValueAsync(newPlayer.PlayerToJson());

        // Update auth player with new display name -> tagging along username inout field
        UpdatePlayerDisplayName(displayName);
    }

    public async Task UpdatePlayerDisplayName(string displayName)
    {
        if(auth.CurrentUser != null)
        {
            UserProfile profile = new UserProfile
            {
                DisplayName = displayName
            };
            auth.CurrentUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was camcelled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error" + task.Exception);
                    return;
                }
                Debug.Log("User profile updated successfully");
                Debug.LogFormat("Checking current user display name from auth {0}", GetCurrentUserDisplayName());
            });
        }
    }
    public string GetCurrentUserDisplayName()
    {
        return auth.CurrentUser.DisplayName;
    }
    
    public FirebaseUser GetCurrentUser()
    {
        return auth.CurrentUser;
    }

    public void SignOut()
    {
        Debug.Log("signing out");
        if (auth.CurrentUser != null)
        {
            Debug.LogFormat("Signing out {0}, {1}", auth.CurrentUser.UserId, auth.CurrentUser.Email);

            //get current index of scene
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            auth.SignOut();
            if (currentSceneIndex !=0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void ForgetPassword()
    {
        string email = emailInput.text.Trim();

        auth.SendPasswordResetEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("Sorry, sending reset password to email was canceled");
                return;
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Sorry, sending a reset password to email was faulted: " + task.Exception);
                return;
            }

            else if (task.IsCompleted)
            {
                // Firebase user has been created.
                Debug.LogFormat("Forget password email sent successfully: {0} {1}");
            }

            Debug.Log("forget password");
        });
    }

    public bool ValidateEmail(string email)
    {
        bool isValid = false;

        //all emails has @ sign
        const string pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
        const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
        

        if (email !="" && Regex.IsMatch(email,pattern,options))
            {
            isValid = true;
        }

        return isValid;
    }

    public bool ValidatePassword(string password)
    {
        bool isValid = false;

        if(password !="" && password.Length>=6)
        {
            isValid = true;
        }

        return isValid;
    }

    /*public bool ValidateUsername(string username)
    {
        bool isValid = false;
        //Regex only contains letters,underscores and dots
        const string uPattern = "^(?=[a - zA - Z0 - 9._]{ 3,20}$)(? !.*[_.]{ 2})[^_.].*[^_.]$";

        if (username != "" && Regex.IsMatch(username, uPattern))
        {
            isValid = true;
        }

        return isValid;
    }
    */
    public string HandleSignUpError(Task<FirebaseUser> task)
    {
        string errorMsg = "";

        if (task.Exception != null)
        {
            FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            errorMsg = "Sign up Fail\n";

            switch(errorCode)
            {
                case AuthError.EmailAlreadyInUse:
                    errorMsg += "Email already in use.";
                    break;
                case AuthError.WeakPassword:
                    errorMsg += "Password is weak.";
                    break;
                case AuthError.InvalidEmail:
                    errorMsg += "Email is invalid.";
                    break;
                default:
                    errorMsg += "Issues in authentication";
                    break;
            }

            Debug.LogError("Error message" + errorMsg);
        }

        return errorMsg;
    }

    public string HandleSignInError(Task<FirebaseUser> task)
    {
        string errorMsg = "";
        if (task.Exception != null)
        {
            FirebaseException firebaseEx = task.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            errorMsg = "Sign In Fail\n";

            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    errorMsg += "Missing an email";
                    break;
                case AuthError.WrongPassword:
                    errorMsg += "Password is wrong.";
                    break;
                case AuthError.InvalidEmail:
                    errorMsg += "Email is invalid.";
                    break;
                case AuthError.UserNotFound:
                    errorMsg += "User is not found.";
                    break;
                default:
                    errorMsg += "Issues in authentication";
                    break;
            }

            Debug.LogError("Error message" + errorMsg);
        }

        return errorMsg;
    }
}