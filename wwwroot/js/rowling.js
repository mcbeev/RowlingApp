window.rowling = {
    updateTeamScore: function (teamSlug, currentScore) {
        //document.getElementById('team-' + teamSlug + '-current-score').innerText = currentScore;
    },
    removeJumbo: function () {
        document.getElementById('main-banner').style.display = 'hidden';
    },
    //displayWelcome: function (welcomeMessage) {
    //    document.getElementById('welcome').innerText = welcomeMessage;
    //},
    //returnArrayAsyncJs: function () {
    //    DotNet.invokeMethodAsync('BlazorSample', 'ReturnArrayAsync')
    //        .then(data => {
    //            data.push(4);
    //            console.log(data);
    //        });
    //},
    //sayHello: function (dotnetHelper) {
    //    return dotnetHelper.invokeMethodAsync('SayHello')
    //        .then(r => console.log(r));
    //}
    //wireUpEvents: function () {
    //    document.getElementById('main-banner').addEventListener('click', this.removeJumbo);
    //},
    //init: function () {
    //    this.wireUpEvents();
    //}
};
