var Cells = [];


// Initialize the SignalR client
var connection = new signalR.HubConnectionBuilder()
    .withUrl('/Calendar')
    .build();

//Here we tell the SignalR What to do when messages come from the hub
connection.on('ReceiveCell', renderCell2);
connection.on('NewAttendee', getAttendeeAttribute);
connection.on('ReceiveTempArray', ReceiveTempArray);

function ReceiveTempArray(TempArray) {
    TempAttendeeArray = TempArray;
}


//Start The connection 
//connection.start();
connection.onclose(function () {
    onDisconnected();
    console.log('Reconnecting in 5 sec ...');
    setTimeout(startSignalRConnection, 5000);
})

//Function that Start The connection with the Hub  
function startSignalRConnection() {
    connection.start()
        .then(onConnected)
        .catch(function (err) {
            console.log(err);
        });
    
}

//function That update UI if the connection status changed 
function onDisconnected() {
    var connectionInfoDialog = document.getElementById("connection-info");
    if (connectionInfoDialog.classList.contains('connected')) {
        connectionInfoDialog.classList.remove('connected');
    }
    connectionInfoDialog.classList.add('disconnected');
}

function onConnected() {
    var connectionInfoDialog = document.getElementById("connection-info");
    if (connectionInfoDialog.classList.contains('disconnected')) {
        connectionInfoDialog.classList.remove('disconnected');
    }
    connectionInfoDialog.classList.add('connected');
}


//Method in client side invoke the Hub Method 
function sendCell(id) {
    connection.invoke('SendCell',id);
}

//Creat EventListener to all available cells in the table 
function createEventListeners() {
    var j = 0;
    var i = 0;
    //Make Cells array ready
    for (i = 0; i <= interval; i++) {
        Cells[i] = [];
    }
    var tampCellToGetId;
    do {
        for (i = 0; i < 24; i++) {
            tampCellToGetId = document.getElementById(j + "-" + i);
            Cells[j][i] = tampCellToGetId.id;
            var cellEl = document.getElementById(Cells[j][i]);
            console.log(Cells[j][i]);
            cellEl.addEventListener("click", function () {
                var currentAttendeeArray = [];
                currentAttendeeArray = getCellUsers(this.id);
                AddaptAttendeeArray(currentAttendeeArray, GlobalAttendee);
                var attendeeCaller = GlobalAttendee;
                connection.invoke('FillTempArray', TempAttendeeArray);
                sendCell(this.id);
            });
        }
        j++;
    } while (j <= interval);
}

//The function that we call once the page loaded 
function ready() { 
    
    
    createEventListeners();


    //Get The name of the attendee 
    var welcomePanel = document.getElementById('welcome-panel');
    welcomePanel.addEventListener('submit', function (e) {
        e.preventDefault();
        var name = e.target[0].value;
        if (name && name.length) {
            welcomePanel.style.display = 'none';
            GlobalAttendee = name;
            startSignalRConnection();            
        }
    });    

    //adapt welcome panel size 
    var calendarH = document.getElementsByTagName('table').clientHeight;
    welcomePanel.style.height = calendarH;
}


//Functions that Hub invoke 
function renderCell2(id) {
    fillCell(id);
}

function getAttendeeAttribute() {
    //alert('Hello new attendee');
    //connection.invoke('FillAttendeeList', GlobalAttendee);
    //connection.invoke('FillGlobalArray');
}


document.addEventListener('DOMContentLoaded', ready);