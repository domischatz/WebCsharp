$.get("/api/GetAllRoles", function (data) {
    $.each(data, function (index, item) {
        $("#roles").append(new Option(item, index));
    });
    $("#roles").append(new Option("Alle Rollen"));
});

$("#roles").on("change", function (e) {
    let selected = $(this).find('option:selected').text();
    $.get("/api/GetUserOfRole", { roleString: selected }, function (data) {
        $("table tbody").empty()
        $.each(data, function (index, item) {
            $("table tbody").append(`
            <tr>
                <td>${item.email}</td>
                <td>${item.name}</td>
                <td>${new Date(item.birthdate).toDateString()}</td>
                <td><button class="delete-btn" data-email="${item.email}">Löschen</button></td>
            </tr>`);
        });
    });
});

// Eventhandler für das Löschen von Benutzern
$("table").on("click", ".delete-btn", function () {
    let email = $(this).data("email");
    if (confirm("Möchten Sie diesen Benutzer wirklich löschen?")) {
        $.ajax({
            url: `/api/delete/${email}`,
            type: "DELETE",
            success: function () {
                // Benutzer erfolgreich gelöscht, aktualisiere die Benutzerliste
                refreshUserList();
            },
            error: function () {
                // Fehler beim Löschen des Benutzers
                alert("Fehler beim Löschen des Benutzers.");
            }
        });
    }
});

// Funktion zum Aktualisieren der Benutzerliste nach dem Löschen eines Benutzers
function refreshUserList() {
    let selectedRole = $("#roles").find('option:selected').text();
    $.get("/api/GetUserOfRole", { roleString: selectedRole }, function (data) {
        $("table tbody").empty();
        $.each(data, function (index, item) {
            $("table tbody").append(`
            <tr>
                <td>${item.email}</td>
                <td>${item.name}</td>
                <td>${new Date(item.birthdate).toDateString()}</td>
                <td><button class="delete-btn" data-email="${item.email}">Löschen</button></td>
            </tr>`);
        });
    });
}
