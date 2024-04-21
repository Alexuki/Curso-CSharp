var ContactListApiClient = function (apiBaseUri) {
    this.reloadContacts = function () {
        return $.ajax({
            url: apiBaseUri,
            type: 'get'
        });
    };

    this.deleteContact = function (contactId) {
        return $.ajax({
            url: apiBaseUri+contactId,
            type: 'delete'
        });
    };

    this.getContact = function (contactId) {
        return $.ajax({
            url: apiBaseUri + contactId,
            type: 'get'
        });
    };

    this.createContact = function(name, phone) {
        return $.ajax({
            url: apiBaseUri,
            type: 'post',
            data: JSON.stringify({ name: name, phone: phone }),
            contentType: 'application/json; charset=utf-8'
        });
    };
};


