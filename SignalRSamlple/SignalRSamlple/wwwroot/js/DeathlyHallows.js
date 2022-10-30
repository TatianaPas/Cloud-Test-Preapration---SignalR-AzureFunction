// create connection

var connectionDeathlyHallows = new signalR.HubConnectionBuilder().withUrl("/hubs/DeathlyHallows").build();

//connect to methods that hub invokes, receive notifications from hub
connectionDeathlyHallows.on("updateDeathlyHallowsCount", (cloak, stone, wand) => {
    var cloakSpan = document.getElementById("cloakCounter");
    var stoneSpan = document.getElementById("stoneCounter");
    var wandSpan = document.getElementById("wandCounter");
    cloakSpan.innerText = cloak.toString();
    stoneSpan.innerText = stone.toString();
    wandSpan.innerText = wand.toString();
});



//invoke hub methods, send notifiction to hub




//start connection

function fulfilled() {
    connectionDeathlyHallows.invoke("GetRaceStatus").then((raceCounter) => {
        var cloakSpan = document.getElementById("cloakCounter");
        var stoneSpan = document.getElementById("stoneCounter");
        var wandSpan = document.getElementById("wandCounter");
        cloakSpan.innerText = raceCounter.cloak.toString();
        stoneSpan.innerText = raceCounter.stone.toString();
        wandSpan.innerText = raceCounter.wand.toString();
    });


    console.log("Connection to User HubSuccessfull");
    NewWindowLoadedOnClient();

}

function rejected() {
    alert("Connection to User Hub Rejected");
}


connectionDeathlyHallows.start().then(fulfilled, rejected);