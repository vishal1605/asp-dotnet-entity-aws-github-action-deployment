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

editor.on('preSubmit', (e, o, action) => {
    if (action !== 'remove') {
        let userName = editor.field('userName');
        let password = editor.field('password');
        let gender = editor.field('gender');
        let age = editor.field('age');


        if (!age.val()) {
            age.error(
                "Please Eneter Value"
            );
        }

        if (isNaN(age.val())) {
            age.error(
                "Please Eneter Numeric"
            );
        }
        if (!userName.val()) {
            userName.error(
                "Please Eneter Value"
            );
        }
        if (!password.val()) {
            password.error(
                "Please Eneter Value"
            );
        }
        if (!gender.val()) {
            gender.error(
                "Please Eneter Value"
            );
        }
        
        

        if (editor.inError()) {
            return false;
        }

    }
})