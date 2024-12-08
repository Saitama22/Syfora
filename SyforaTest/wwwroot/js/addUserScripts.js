function showAddModal() {
    document.getElementById('confirm-add-modal').style.display = 'block';
}

function closeAddModal() {
    const errorMessageElement = document.getElementById('confirm-add-message');
    errorMessageElement.style.display = 'none';
    errorMessageElement.textContent = '';
    document.getElementById('confirm-add-modal').style.display = 'none';    
}

function addRecord() {
    const login = document.getElementById('login-add-input').value;
    const firstName = document.getElementById('firstName-add-input').value;
    const lastName = document.getElementById('lastName-add-input').value;
    const userData = {
        login: login,
        firstName: firstName,
        lastName: lastName
    };

    fetch(`/api/Users`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
    }).then(response => {
        if (!response.ok) {
            const errorMessageElement = document.getElementById('confirm-add-message');
            errorMessageElement.style.display = 'block';
            response.text().then(data => {
                errorMessageElement.textContent = data;
            });
        }
        else {
            closeAddModal();
            location.reload();
        }
    });
}
