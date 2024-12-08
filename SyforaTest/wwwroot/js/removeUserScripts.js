let userIdToDelete = null;

function showRemoveModal(userId) {
    userIdToDelete = userId;
    document.getElementById('confirm-remove-modal').style.display = 'block';
}

function closeRemoveModal() {
    const errorMessageElement = document.getElementById('confirm-remove-message');
    errorMessageElement.style.display = 'none';
    errorMessageElement.textContent = '';
    document.getElementById('confirm-remove-modal').style.display = 'none'; 
    userIdToDelete = null;
}

function removeRecord() {
    fetch(`/api/Users/${userIdToDelete}`, {
        method: 'DELETE', 
    }).then(response => {
        if (!response.ok) {
            const errorMessageElement = document.getElementById('confirm-remove-message');
            errorMessageElement.style.display = 'block'; 
            response.text().then(data => {
                errorMessageElement.textContent = data;
            });
        }
        else {
            closeRemoveModal();
            location.reload();
        }
    }); 
}
