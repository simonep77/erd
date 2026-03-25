// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Attiva il loader sul selettore specificato. Se selettore null allora attiva loader a livello di pagina
function loaderOn(selectorWaiter) {
    let jqTarget = (selectorWaiter == null) ? $(document.body) : $(selectorWaiter);
    let jqLoader = jqTarget.children("div.loader");

    if ((jqLoader).length == 0)
        jqTarget.append(buildLoader());
}

//Disattiva il loader sul selettore specificato. Se selettore null disattiva attiva loader a livello di pagina
function loaderOff(selectorWaiter) {

    let jqTarget = (selectorWaiter == null) ? $(document.body) : $(selectorWaiter);
    let jqLoader = jqTarget.children("div.loader");

    if ((jqLoader).length > 0)
        jqLoader.remove();
}

function buildLoader() {

    let divLoader = '';

    divLoader += '<div class="loader" style="position: fixed; display: block; width: 100%; height: 100%; top: 0; left: 0; right: 0; bottom: 0; background-color: rgba(0,0,0,0.5); z-index: 99; cursor: pointer" onclick="off()">';
    divLoader += '  <svg version="1.1" id="loader-1" xmlns="http://www.w3.org/2000/svg" style="margin-left:50%;margin-top:10%" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px"';
    divLoader += '     width="40px" height="40px" viewBox="0 0 50 50" style="enable-background:new 0 0 50 50;" xml:space="preserve">';
    divLoader += '  <path fill="#FF6700" d="M25.251,6.461c-10.318,0-18.683,8.365-18.683,18.683h4.068c0-8.071,6.543-14.615,14.615-14.615V6.461z">';
    divLoader += '    <animateTransform attributeType="xml" ';
    divLoader += '      attributeName="transform" ';
    divLoader += '      type="rotate" ';
    divLoader += '      from="0 25 25" ';
    divLoader += '      to="360 25 25" ';
    divLoader += '      dur="0.8s" ';
    divLoader += '      repeatCount="indefinite"/>';
    divLoader += '    </path>';
    divLoader += '  </svg>';
    divLoader += '</div>                                                                                                                                                                                                    ';

    return divLoader;
}

function showAlert(alertclass, title, text, callback) {

    let mydialog = ` 
    <div class="modal fade" id="modalalert" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true"> 
        <div class="modal-dialog modal-dialog-centered" role="document"> 
            <div class="modal-content"> 
                <div class="modal-header " style="text-align:left"> 
                    <h4 class="modal-title">${title}</h4> 
                    <button type="button" class="close" data-dismiss="modal">&times;</button> 
                </div> \
                <div class="modal-body" style="text-align:left"> 
                    <div class="alert ${alertclass}">
                    ${text} 
                    </div>

                </div> 

                <div class="modal-footer" style="text-align:center"> 
                    <button type="button" class="btn btn-primary" id="btnModalAlert"  name="ConfirmBtnDelete" >OK</button> 
                </div> 
            </div> 
        </div> 
    </div> 
    `;

    $("#modalalert").remove();
    var diag = $(mydialog).appendTo(document.body);
    $("#modalalert #btnModalAlert").on("click", function () {
        $("#modalalert").modal('hide');
        callback();
    })
    diag.modal('show');
}


//Scarica file da javascript
function downloadFile(url, filename) {
    // show the loading indicator
    loaderOn();

    fetch(url)
        .then(resp => resp.blob())
        .then(file => {
            // HANDLE FILE
            const url = window.URL.createObjectURL(file);
            const a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;
            a.download = filename;
            document.body.appendChild(a);
            a.click();
            window.URL.revokeObjectURL(url);
            a.remove();
        })
        .catch(() => {
            // LOG ERROR
            console.log(e);
            alert("Errore in download del file!");
        })
        .finally(() => {
            // STOP LOADING
            loaderOff();
        });

}



//Dato un json di tipo ResponseBase valuta messaggi ed errori
function evaluateJsonResponseNew(data) {
    if (data == null) {
        showAlert("danger", "Errore", "Nessun dato in risposta", function () { }); 
        return false;
    }

    if (data.HasErrors == null) {
        return true;
    }

    //Problemi...
    if (data.HasErrors) {
        //console.log(data);
        showAlert("danger", "Errore", data.Messages[0].Text, function () { }); 
    }

    return !data.HasErrors;
}


//Funzione standardizzata per le richieste GET Ajax
function ajaxGetAsync(urlGet, dataObj, callbackSuccess, callbackError, selectorWaiter) {

    if (selectorWaiter !== 'none')
        loaderOn(selectorWaiter);

    var jqXhr = $.ajax({
        async: true,
        cache: false,
        type: "GET",
        url: urlGet,
        datatype: "json",
        data: dataObj,
        content: "application/json; charset=iso-8859-1",
        xhrFields: { "LoaderSelector": selectorWaiter }
    })
        .done(function (data, textStatus, jqXHR) {
            //Spegne il loader
            loaderOff(jqXHR.LoaderSelector);

            //console.log(data);
            //console.log(jqXHR);

            if (data == "Errore!!!") {
                showAlert("danger", "Errore", "Errore durante l'operazione", function () { }); 
                return false;
            }

            if (evaluateJsonResponseNew(data)) {
                callbackSuccess(data);
            }
            else {
                if (callbackError != null) {
                    callbackError(data);
                }
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            //Spegne il loader
            loaderOff(jqXHR.LoaderSelector);
            showAlert("danger", "Errore", textStatus + "<br />" + errorThrown, function () { }); 

            if (callbackError != null) {
                callbackError();
            }
        });

    //console.log(jqXhr);
}

//Funzione standardizzata per le richieste GET Ajax
function ajaxPostAsync(urlGet, dataObj, callbackSuccess, callbackError, selectorWaiter) {

    if (selectorWaiter !== 'none')
        loaderOn(selectorWaiter);
    
    var jqXhr = $.ajax({
        async: true,
        cache: false,
        method: "POST",
        url: urlGet,
        //dataType: "json",
        data: JSON.stringify(dataObj),
        headers: { 'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() },
        contentType: 'application/json',
        xhrFields: { "LoaderSelector": selectorWaiter }
    })
        .done(function (data, textStatus, jqXHR) {
            //Spegne il loader
            loaderOff(jqXHR.LoaderSelector);

            //console.log(data);
            //console.log(jqXHR);

            if (data == "Errore!!!") {
                showAlert("danger", "Errore", "Errore durante l'operazione", function () { }); 
                return false;
            }

            if (evaluateJsonResponseNew(data)) {
                callbackSuccess(data);
            }
            else {
                if (callbackError != null) {
                    callbackError(data);
                }
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            //Spegne il loader
            loaderOff(jqXHR.LoaderSelector);
            showAlert("danger", "Errore", textStatus + "<br />" + errorThrown, function () { }); 

            if (callbackError != null) {
                callbackError();
            }
        });

    //console.log(jqXhr);
}


function ajaxPostForm(urlGet, dataObj, callbackSuccess, callbackError, selectorWaiter) {

    loaderOn(selectorWaiter);

    var jqXhr = $.ajax({
        headers: {
            RequestVerificationToken:
                $('input:hidden[name="__RequestVerificationToken"]').val()
        },
        async: true,
        processData: false,
        contentType: false,
        cache: false,
        type: "POST",
        url: urlGet,
        data: dataObj,
        content: "application/json; charset=iso-8859-1",
        xhrFields: { "LoaderSelector": selectorWaiter }
    })
        .done(function (data, textStatus, jqXHR) {
            //Spegne il loader
            loaderOff(jqXHR.LoaderSelector);

            if (data == "Errore!!!") {
                showAlert("danger", "Errore", "Errore durante l'operazione", function () { }); 
                return false;
            }

            if (evaluateJsonResponseNew(data)) {
                callbackSuccess(data);
            }
            else {
                if (callbackError != null) {
                    callbackError(data);
                }
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            //Spegne il loader
            loaderOff(jqXHR.LoaderSelector);
            showAlert("danger", "Errore", textStatus + "<br />" + errorThrown, function () { }); 

            if (callbackError != null) {
                callbackError();
            }
        });


}