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

    $('#btnStart_' + id).attr('disabled', true);

    checkPermissions(function () {
        $.ajax({
            type: 'PATCH',
            url: '/Tour/StartTour',
            data: {
                id: id
            },
            success: function (data) {
                if (!data)
                    console.log('Error! Bitte kontaktieren Sie einen Administrator!');
                $('#btnStart_' + id).attr('disabled', false);
            },
            error: function () {
                console.log('Error! Bitte kontaktieren Sie einen Administrator!');
                $('#btnStart_' + id).attr('disabled', false);
            }
        });
    });
}

function completeTourAjax(id) {
    if (!id)
        alert('Tour konnte nicht beendet werden! Bitte wenden Sie sich an einen Administrator!');

    $('#btnEnd_' + id).attr('disabled', true);

    checkPermissions(function () {
        $.ajax({
            type: 'PATCH',
            url: '/Tour/CompleteTour',
            data: {
                id: id
            },
            success: function (data) {
                if (!data)
                    console.log('Error! Bitte kontaktieren Sie einen Administrator!');
                $('#btnEnd_' + id).attr('disabled', false);
            },
            error: function () {
                console.log('Error! Bitte kontaktieren Sie einen Administrator!');
                $('#btnEnd_' + id).attr('disabled', false);
            }
        });
    });
}

function cancelTourAjax(id) {
    checkPermissions(function () {
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
    });
}

function checkPermissions(callback) {
    var teacherId = getTeacherId();
    if (teacherId) {
        $.ajax({
            type: 'POST',
            url: '/Tour/CheckPermissions',
            data: {
                teacherId: teacherId
            },
            success: function (teacherId) {
                if (teacherId === 'BigFail') {
                    checkPermissionsWithPin(callback);
                } else if (teacherId && teacherId.length === 3) {
                    callback();
                }
                    
            },
            error: function () {
                console.log('Error! Bitte kontaktieren Sie einen Administrator!');
            }
        });
    } else {
        checkPermissionsWithPin(callback);
    }
}

function checkPermissionsWithPin(callback) {
    bootbox.prompt({
        title: "Teacher PinCode",
        buttons: {
            cancel: {
                label: 'Abbrechen',
                className: 'bg-ternary-color text-white'
            },
            confirm: {
                label: 'Bestätigen',
                className: 'bg-ternary-color text-white'
            }
        },
        inputType: 'password',
        callback: function (pinCode) {
            if (pinCode) {
                $.ajax({
                    type: 'POST',
                    url: '/Tour/CheckPermissions',
                    data: {
                        pinCode: pinCode
                    },
                    success: function (teacherId) {
                        if (teacherId === 'BigFail') {
                            alert("Pincode falsch!");
                        } else if (teacherId && teacherId.length === 3) {
                            setTeacherId(teacherId);
                            callback();
                        }

                    },
                    error: function () {
                        console.log('Error! Bitte kontaktieren Sie einen Administrator!');
                    }
                });
            }
        }
    });
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
        '<button id="btnStart_' + id + '" type="button" class="btn bg-primary-color text-white w-100 font-weight-bold"' +
        'onclick="startTourAjax(\'' + id + '\');">Bestätigen</button>' +
        '</div>' +
        '</div>' +
        '</div >';
}

function buildStartedTourPanel(id, guideName, guideTeam, visitorName, startTime) {
    if (!visitorName)
        visitorName = '--';
    var date = new Date(startTime);
    var hour = ("0" + date.getHours()).slice(-2);
    var minute = ("0" + date.getMinutes()).slice(-2);
    return '<div id="tourStarted_' + id + '" class="col-12 col-lg-6 mt-3" >' +
        '<div class="card">' +
        '<div class="card-header bg-secondary-color h4">' + guideName +
        '<a onclick="cancelTourAjax(\'' + id + '\')">' +
            '<i class="fa fa-1x fa-times fa-pull-right text-dark"></i>'+
        '</a>' +
        '</div>' +
        '<div class="card-body bg-ternary-color text-white">' +
        '<p>' +
        '<strong>Team:</strong> ' + guideTeam + '<br />' +
        '<strong>Führungsstart:</strong> ' + hour + ':' + minute + '<br />' +
        '<strong>Gast:</strong> ' + visitorName +
        '</p>' +
        '<button id="btnEnd_' + id + '" type="button" class="btn bg-primary-color text-white w-100 font-weight-bold"' +
        'onclick="completeTourAjax(\'' + id + '\');">Beenden</button>' +
        '</div>' +
        '</div>' +
        '</div >';
}