<!-- ABOUT THE PROJECT -->

## About The Project

5HEAD DDA WEBSITE



### Built With
-   [Firebase](https://firebase.google.com/docs/web/setup)
-   [Login Template](https://colorlib.com/wp/template/login-form-v1/)
-   [HTML](https://www.w3schools.com/html/)
-   [CSS](https://www.w3schools.com/css/)
-   [JavaScript](https://www.w3schools.com/js/)
-   [Bootstrap](https://getbootstrap.com)


<!-- GETTING STARTED -->

## Getting Started

Sign up for an account in the game and you should be able to log in on the website itself. Open the index.html with a browser and you should be able to sign in and see your own data!

### Prerequisites
Install a web browser to view the website, and install the application to sign up for an account

<!-- USAGE EXAMPLES -->

## Usage

1. The project gets the user's credentials to log in
* Log in code snippet
```
   const email = loginForm['email'].value;
   const password = loginForm['password'].value;
   firebase.auth().setPersistence(firebase.auth.Auth.Persistence.LOCAL)
     .then(() => {
       return firebase.auth().signInWithEmailAndPassword(email, password).then(cred => {
         ...
         })
   ```

2. Retrieves all keys from database
   * Retrieving all players' key code snippet
   ```
   const playerRef = firebase.database().ref("playerStats").orderByKey();
   //if there is a value, run function playerKey
   playerRef.once("value").then(function(playerKey) {
     //for every value in playerKey, get a childSnapshot "playerKey/$playerKey"
     playerKey.forEach(function(childSnapshot) {
       //let key be the value
       let key = childSnapshot.key;
   ```

3. Gets individual data from database
   * Retrieving data (accuracy) key code snippet
   ```
   const playerRef = firebase.database().ref("playerStats/" + uid + "/accuracy");
   //PlayerRef is declared at the top using a constant
   let accuracyContent = "";
   playerRef.once("value")
     .then(function(snapshot) { //retrieve a snapshot of the data using a callback
       if (snapshot.exists()) { //if the data exist
         try {
           //let's do something about it

           let accuracyVal = snapshot.val();
   ```

<!-- ROADMAP -->

## Roadmap for website

-   [x] Add Website
-   [x] Add Authentication
-   [x] Retrieving from database
-   [ ] Adding image
-   [ ] Adding leaderboard names
-   [ ] Adding feedback form

<!-- CONTRIBUTING -->

## Contributing Members
Every function and script used in the website except for Login templates are coded by me. However, data that could be retrieve and application sign up is done by Charlene Soh.


<!-- CONTACT -->

## Contact

Your Name - Darrence Tan) - s10194154@connect.np.edu.sg

Project Link: <https://github.com/bryangregory-hub/IP-Asg2.git>

<!-- ACKNOWLEDGMENTS -->

## Acknowledgments

Use this space to list resources you find helpful and would like to give credit to. I've included a few of my favorites to kick things off!

-   [GitHub Pages](https://pages.github.com)
-   [Font Awesome](https://fontawesome.com)
-   [Stack Overflow](https://stackoverflow.com)
-   [Firebase Documentation](https://firebase.google.com/docs/reference/js/v8)
-   [The Net Ninja](https://www.youtube.com/channel/UCW5YeuERMmlnqo4oq8vwUpg)
