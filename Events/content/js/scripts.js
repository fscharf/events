// custom bootstrap patterns
$('.card').addClass('bg-white border-0');

$(".card").hover(function () {
    $(this).toggleClass("text-primary shadow-lg");
});

$("#favToggler").click(function () {
    $(this).toggleClass("text-danger");
});

$(".bg-search").focus(function () {
    $(this).remove("bg-search");
});

// spinner button
$('.btn-preload').click(function () {
    $(this).html('<small class="spinner-border" role="status" aria-hidden="true"></small>').addClass('disabled');
});

// compartilhar evento
$('#shareLinkAlert').hide();
$('#shareLinkBtn').click(function () {
    //Visto que o 'copy' copia o texto que estiver selecionado, talvez você queira colocar seu valor em um txt escondido
    $('#shareLink').select();
    var ok = document.execCommand('copy');
    if (ok) {
        $('#shareLinkAlert').show();
    }
});

// acionar todos tooltips
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

// mostrar/ocultar navbar (paliativo pois navbar-toggler não funciona)
$(".nav-toggler").click(function () {
    $(".navbar-collapse").toggle("fast");
});

// pegamos o valor no localStorage
const nightModeStorage = localStorage.getItem('gmtNightMode')
const nightMode = document.querySelector('#night-mode')

// caso tenha o valor no localStorage
if (nightModeStorage) {
    // ativa o night mode
    document.documentElement.classList.add('night-mode')

    // já deixa o input marcado como ativo
    nightMode.checked = true
}

// ao clicar mudaremos as cores
nightMode.addEventListener('click', () => {
    // adiciona a classe `night-mode` ao html
    document.documentElement.classList.toggle('night-mode')

    // se tiver a classe night-mode
    if (document.documentElement.classList.contains('night-mode')) {
        // salva o tema no localStorage
        localStorage.setItem('gmtNightMode', true)
        return
    }
    // senão remove
    localStorage.removeItem('gmtNightMode')
})