const deleteModal = document.getElementById('deleteModal')

deleteModal.addEventListener('show.bs.modal', (event) => {
    const button = event.relatedTarget;
    const id = button.getAttribute('data-id')
    const input = deleteModal.querySelector("#deleteId")
    input.value = id
})


const tempDataMessage = document.getElementById('tempDataMessage')

if (tempDataMessage) {
    setTimeout(() => {
        tempDataMessage.style.transition = 'opacity .5s';
        tempDataMessage.style.opacity = '0';
        setTimeout(() => tempDataMessage.remove(), 500);
    }, 3000)

}
