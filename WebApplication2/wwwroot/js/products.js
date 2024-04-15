$(document).ready(function () {
    $('#productsTable').DataTable({
        "ajax": {
            "url": "/Products/GetProducts",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "autoWidth": true },
            { "data": "price", "autoWidth": true },
            { "data": "category", "autoWidth": true },
            {
                "data": "id", "width": "50px", "render": function (data) {
                    return '<a class="btn btn-success" href="/Products/Edit/' + data + '"><i class="bi bi-pencil-fill"></i></a>';
                }
                //inline ask what is bootstrap edit icon
            },
            {
                //q: what is bootstrap details icon syntax
                //a: <i class="bi bi-info-circle-fill"></i>

                "data": "id", "width": "50px", "render": function (data) {
                    return '<a class="btn btn-primary" href="/Products/Details/' + data + '"><i class="bi bi-info-circle-fill"></i></a>';
                }
            },
            {
                //q: what is bootstrap trash icon
                //a: <i class="bi bi-trash-fill"></i>
                "data": "id", "width": "50px", "render": function (data) {
                    return '<button class="btn btn-danger" onclick="deleteProduct(' + data + ')"><i class="bi bi-trash-fill"></i></button>';
                }
            }
        ]
    });

});
//  add a confirm box and on confirmation invoke a controller endpoint to delete product
//add sweetlaert confirm box -- so this by selecting the
function deleteProduct(id) {
    swal({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover this product!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: 'Products/DeleteProduct/' + id,
                type: 'DELETE',
                success: function (result) {
                    $('#productsTable').DataTable().ajax.reload();
                }
            });
        }
    });
}