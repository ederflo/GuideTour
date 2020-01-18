'use strict';

var hubConnection = new signalR.HubConnectionBuilder().withUrl("/tourHub").build();

function startConnection() {
    console.log('connecting...');
    hubConnection.start()
        .then(function () { console.log('connected!'); })
        .catch(function (err) {
            console.error(err);
            setTimeout(function () { startConnection(); }, 2000);
        });
}

hubConnection.onclose(function () {
    console.log('disconnected');
    setTimeout(function () { startConnection(); }, 2000);
});

startConnection();

hubConnection.on("NewRequestedTour", function (data) {
    addNotStartedTour(data);
});

hubConnection.on("TourStarted", function (data) {
    $('#tourNotStarted_' + data.id).remove();
    addStartedTour(data);
});

hubConnection.on("TourCompleted", function (data) {
    $('#tourStarted_' + data.id).remove();
});

hubConnection.on("TourCancelled", function (data) {
    $('#tourStarted_' + data.id).remove();
});

function startTourAjax(id) {
    if (!id)
        alert('Tour konnte nicht gestartet werden! Bitte wenden Sie sich an einen Administrator!');

    $.ajax({
        type: 'PATCH',
        url: '/Tour/StartTour',
        data: {
            id: id
        },
        success: function (data) {
            if (!data)
                console.log('Error! Bitte kontaktieren Sie einen Administrator!');
        },
        error: function () {
            console.log('Error! Bitte kontaktieren Sie einen Administrator!');
        }
    });
}

function completeTourAjax(id) {
    if (!id)
        alert('Tour konnte nicht beendet werden! Bitte wenden Sie sich an einen Administrator!');

    $.ajax({
        type: 'PATCH',
        url: '/Tour/CompleteTour',
        data: {
            id: id
        },
        success: function (data) {
            if (!data)
                console.log('Error! Bitte kontaktieren Sie einen Administrator!');
        },
        error: function () {
            console.log('Error! Bitte kontaktieren Sie einen Administrator!');
        }
    });
}

function cancelTourAjax(id) {
    var doDelete = confirm("Wollen Sie diese Tour wirklich abbrechen? Diese kann nicht zurückgesetzt werden!");
    if (doDelete === true) {
        if (!id)
            alert('Tour konnte nicht abgebrochen werden! Bitte wenden Sie sich an einen Administrator!');

        $.ajax({
            type: 'PATCH',
            url: '/Tour/CancelTour',
            data: {
                id: id
            },
            success: function (data) {
                if (!data)
                    console.log('Error! Bitte kontaktieren Sie einen Administrator!');
            },
            error: function () {
                console.log('Error! Bitte kontaktieren Sie einen Administrator!');
            }
        });
    }
}

function addNotStartedTour(data) {
    var panel = buildNotStartedTourPanel(data.id, data.guideName, data.team, data.visitorName);
    $("#notStartedTours").append(panel);
}

function addStartedTour(data) {
    var panel = buildStartedTourPanel(data.id, data.guideName, data.team, data.visitorName, data.startedTour);
    $("#startedTours").append(panel);
}

function buildNotStartedTourPanel(id, guideName, guideTeam, visitorName) {
    if (!visitorName)
        visitorName = '--';
    return '<div id="tourNotStarted_' + id + '" class="col-12 col-lg-6 mt-3" >' +
        '<div class="card">' +
        '<div class="card-header bg-secondary-color h4">' + guideName + '</div>' +
        '<div class="card-body bg-ternary-color text-white">' +
        '<p>' +
        '<strong>Team:</strong> ' + guideTeam + '<br />' +
        '<strong>Gast:</strong> ' + visitorName +
        '</p>' +
        '<button type="button" class="btn bg-primary-color text-white w-100 font-weight-bold"' +
        'onclick="startTourAjax(\'' + id + '\');">Bestätigen</button>' +
        '</div>' +
        '</div>' +
        '</div >';
}

function buildStartedTourPanel(id, guideName, guideTeam, visitorName, startTime) {
    if (!visitorName)
        visitorName = '--';
    var date = new Date(startTime);
    return '<div id="tourStarted_' + id + '" class="col-12 col-lg-6 mt-3" >' +
        '<div class="card">' +
        '<div class="card-header bg-secondary-color h4">' + guideName +
        '<a onclick="cancelTourAjax(' + id + ')">' +
            '<i class="fa fa-1x fa-times fa-pull-right text-dark"></i>'+
        '</a>' +
        '</div>' +
        '<div class="card-body bg-ternary-color text-white">' +
        '<p>' +
        '<strong>Team:</strong> ' + guideTeam + '<br />' +
        '<strong>Führungsstart:</strong> ' + date.getHours() + ':' + date.getMinutes() + '<br />' +
        '<strong>Gast:</strong> ' + visitorName +
        '</p>' +
        '<button type="button" class="btn bg-primary-color text-white w-100 font-weight-bold"' +
        'onclick="completeTourAjax(\'' + id + '\');">Beenden</button>' +
        '</div>' +
        '</div>' +
        '</div >';
}