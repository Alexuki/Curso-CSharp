/// <reference path="ContactListApiClient.js" />

var ContactListUi = function(apiBaseUri) {
    var apiClient = new ContactListApiClient(apiBaseUri);
    var $tableBody = $("#contacts-table tbody");
    var $refreshButton = $("#refreshButton");
    var $loading = $("#loading");
    var $nameField = $("#name");
    var $phoneField = $("#phone");
    var self = this;

    self.reloadContactsClicked = function () {
        showLoading(true);
        apiClient.reloadContacts()
            .done(function(result) {
                if (!result || !result.length) {
                    $tableBody.html("<tr><td colspan='3' class='text-center alert alert-warning'>No contacts</td></tr>");
                } else {
                    var rows = "";
                    for (var i = 0; i < result.length; i++) {
                        rows += getContactRow(result[i]);
                    }
                    $tableBody.html(rows);
                }
            }).always(function() {
                showLoading(false);
            });
        return false;
    };

    self.showContactClicked = function () {
        showLoading(true);
        var id = $(this).parents("tr[data-id]").data("id");
        apiClient.getContact(id)
            .done(function (contact) {
                alert("Contact details:\n\n  Id: " + contact.id +
                      "\n  Name: " + contact.name +
                      "\n  Phone: " + contact.phone);
            }).always(function () {
                showLoading(false);
            });
        return false;
    };

    self.deleteContactClicked = function () {
        var id = $(this).parents("tr[data-id]").data("id");
        showLoading(true);
        apiClient.deleteContact(id)
            .done(function() {
                $tableBody.find("tr[data-id=" + id + "]").remove();
            })
            .fail(function(xhr, status, message) {
                if (xhr.status === 404) {
                    alert("Contact not found!");
                } else {
                    alert("Error: " + message);
                }
            })
            .always(function() {
                showLoading(false);
            });
        return false;
    };

    self.createContactClicked = function() {
        var name = $nameField.val();
        var phone = $phoneField.val();
        showLoading(true);
        apiClient.createContact(name, phone)
            .done(function(newContact) {
                $nameField.val("");
                $phoneField.val("");
                $tableBody.append(getContactRow(newContact));
            })
            .fail(function(xhr, status, message) {
                if (xhr.status === 400) {
                    alert("Error creating contact. Verify input data and try again.");
                } else {
                    alert("Error: " + message);
                }
            })
            .always(function() {
                showLoading(false);
            });
        return false;
    };

    // Private functions

    function showLoading(show) {
        if (show) {
            $refreshButton.hide();
            $loading.show();
        } else {
            $loading.hide();
            $refreshButton.show();
        }
    }

    function getContactRow(contact) {
        return "<tr data-id=" + contact.id + ">" +
                            "<td class='text-right'>" + contact.id + "</td>" +
                            "<td><a href='#' class='show'>" + contact.name + "</a></td>" +
                            "<td class='text-right'>" +
                            "<button class='btn btn-sm btn-danger delete'>Delete</button>" +
                            "</td></tr>";
    }
};