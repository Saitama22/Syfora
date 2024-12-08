let userToUpdate = null;

function showUpdateModal(user) {
    console.log(user);
    document.getElementById('id-update-input').value = user.id;
    document.getElementById('login-update-input').value = user.login;
    document.getElementById('firstName-update-input').value = user.firstName;
    document.getElementById('lastName-update-input').value = user.lastName;

    document.getElementById('confirm-update-modal').style.display = 'block';
}

function closeUpdateModal() {
    const errorMessageElement = document.getElementById('confirm-update-message');
    errorMessageElement.style.display = 'none';
    errorMessageElement.textContent = '';
    document.getElementById('confirm-update-modal').style.display = 'none';
}

function updateRecord() {
    const id = document.getElementById('id-update-input').value;
    const login = document.getElementById('login-update-input').value;
    const firstName = document.getElementById('firstName-update-input').value;
    const lastName = document.getElementById('lastName-update-input').value;
    const userData = {
        login: login,
        firstName: firstName,
        lastName: lastName
    };

    fetch(`/api/Users/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    }).then(response => {
        if (!response.ok) {
            const errorMessageElement = document.getElementById('confirm-update-message');
            errorMessageElement.style.display = 'block';
            response.text().then(data => {
                errorMessageElement.textContent = data;
            });
        }
        else {
            closeUpdateModal();
            location.reload();
        }
    });
}
