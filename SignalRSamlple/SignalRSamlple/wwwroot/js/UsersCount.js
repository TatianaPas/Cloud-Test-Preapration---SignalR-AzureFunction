// create connection

var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build();

//connect to methods that hub invokes, receive notifications from hub
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = value.toString();
});

//invoke hub methods, send notifiction to hub

function NewWindowLoadedOnClient() {
    connectionUserCount.send("NewWindowLoaded");
}


//start connection

function fulfilled() {
    console.log("Connection to User HubSuccessfull");
    NewWindowLoadedOnClient();

}

function rejected() {
    alert("Connection to User Hub Rejected");
}


connectionUserCount.start().then(fulfilled, rejected);