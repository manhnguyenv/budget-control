function JsController(url, div, data, method, isLoading) {
    this.url = url;
    this.div = div;
    this.data = data;
    this.method = method;
    this.isLoading = typeof isLoading !== 'undefined' ? isLoading : false;
}

JsController.prototype.callPartialView = function (callback) {
    if(this.isLoading) loading(this.div);

    switch (this.method) {
        case "GET":
            $.get(this.url, this.data)
                .done(response => callback(response));
            break;
        case "POST":
            $.post(this.url, this.data)
                .done(response => callback(response));
            break;
        default:
            break;
    }
};

var loading = function (div) {
    div.html('<img align="center" src="../../Content/img/utils/loading.gif" />');
}