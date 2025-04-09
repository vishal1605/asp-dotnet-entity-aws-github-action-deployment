const editor = new DataTable.Editor({
    ajax: {
        type: "POST",
        url: rootURL + "Home/UserdetailsAJAXPOST",
        data: function (editorData) {
            return {
                requestType: 'UserdetailsProcess',
                requestData: JSON.stringify(editorData)
            }
        },
        success: function (responseObj) {
            console.log(responseObj)
        }
    },
    serverSide: true,
    fields: [
        {
            label: 'Username:',
            name: 'userName'
        },
        {
            label: 'Password:',
            name: 'password',
            type: "password"
        },
        {
            label: 'Gender:',
            name: 'gender'
        },
        {
            label: 'Age:',
            name: 'age'
        }

    ],
    table: '#user-details-table',
    idSrc: 'id'
});
const userDetailsTable = new DataTable('#user-details-table', {
    ajax: {
        type: "GET",
        url: rootURL + "Home/UserdetailsAJAXGET",
        data: {
            requestType: 'GetAllUserDetails',
            requestData: ""
        },
        dataSrc: 'response.data'
    },
    buttons: [
        { extend: 'create', editor },
        { extend: 'edit', editor },
        { extend: 'remove', editor },
    ],
    columns: [
        { data: 'userName' },
        { data: 'password' },
        { data: 'gender' },
        { data: 'age' },
        { data: 'createdDate' },
        { data: 'updatedDate' },
        
    ],
    dom: 'Bfrtip',
    select: true,
    pageLength: 15,
    lengthChange: true,
    lengthMenu: [15, 25, 50, 75, 100],
    //order: [[1, 'desc']],
    // stateSave: true,
    stateDuration: (60 * 60 * 24),

    colReorder: true,

    deferRender: true,

    scrollX: true,
    scrollY: '60vh',
});