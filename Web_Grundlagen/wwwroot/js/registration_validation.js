
$(function () {
    $formDataOk = true; 

    $("#valName").hide();
    $("#valBirthdate").hide();
    $("#valPasswordRetype").hide();
    

    $("#Name").on("blur", function () {
        //alert("Test!!!!")
        let name = $("#Name").val();
        if (name.trim().length < 3) {
            $("#valName").show();
            $formDataOk = false;

        } else {
            $("#valName").hide();
        }

    });

    $("#Birthdate").on("blur", function () {
        if (checkBirthdate($("#Birthdate").val()) === false) {
            $("#valBirthdate").show();
            $formDataOk = false;
        } else {
            $("#valBirthdate").hide();
        }

    });

    // Überprüfung von "Password" und "PasswordRetype"
    $("#PasswordRetype").on("blur", function () {
        if (checkPassword($("#Password").val(), $("#PasswordRetype").val()) === false) {
            $formDataOk = false;
            $("#valPasswordRetype").show();
        } else {
            $("#valPasswordRetype").hide();
        }
    });

   
    $("#btnSubmitRegUpdate").on("click", function (event) {
        if (!$formDataOk) {
            event.preventDefault();
            $formDataOk = true;
        }
    });

});


function checkBirthdate(birthdate) {
    let date = new Date(birthdate);
    let today = new Date();

    if (date > today) {
        return false;
    }
    return true;
}

function checkPassword(pwd, pwdRetype) {
    if (pwd != pwdRetype) {
        return false;
    }
    return true;
}

/*
$(function () {
    $formDataOk = true;

    // Fehlermeldungen für alle Eingabe-Felder hinzufügen ...
    $("#Name").after("<div id='valName'>Name muss mind. 3 Zeichen lang sein!</div>");
    $("#PasswordReType").after("<div id='valPasswordReType'>Passwortkriterien einhalten!</div>");
    $("#Email").after("<div id='valEmail'>Bitte richtige Mail-Adresse angeben!</div>");
    $("#Birthdate").after("<div id='valBirthdate'>Gültiges Geburtsdatum auswählen!</div>");


    $("#Name").on("blur", function () {
        if (checkName($(this).val()) === false) {
            $("#Name").show(); 
            $formDataOk = false;
        } 
    });


    $("#Email").on("blur", function () {
        if (checkEmail($("#Email").val()) === false) {
            $("#PasswordReType").show(); 
            $formDataOk = false;
        }
    });



// Überprüfungsfunktionen
function checkName(name) {
    name = name.trim();
    if (name.length < 3) {
        return false;
    }
    return true;
}



function checkEmail(email) {
    if (!email.includes('@')) {
        return false;
    }
    return true;
}
*/