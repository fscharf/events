$("#favToggler").click(function () {
    $(this).toggleClass("text-danger");
});

// autohide alerts
$(".alert-auto-hide").show().delay(8000).slideToggle(200, function () {
    $(this).addClass('close');
});

// spinner button
$('.btn-preload').click(function () {
    $(this).html('<span class="spinner-border" role="status" aria-hidden="true"></span>').addClass('disabled');
});

$('#myDropdown').on('hide.bs.dropdown', function (e) {
    if (e.clickEvent) {
        e.preventDefault();
    }
});

// compartilhar evento
$('#shareLinkAlert').hide();
$('#shareLinkBtn').click(function () {
    //Visto que o 'copy' copia o texto que estiver selecionado, talvez você queira colocar seu valor em um txt escondido
    $('#shareLink').select();
    var ok = document.execCommand('copy');
    if (ok) {
        $("#shareLinkAlert").show().delay(6000).slideUp(200, function () {
            $(this).addClass('close');
        });
    }
});

// acionar todos tooltips
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

// acionar todos popovers
$(function () {
    $('[data-toggle="popover"]').popover()
})

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 50 || document.documentElement.scrollTop > 50) {
        $('.navbar').addClass('bg-white shadow-sm fixed-top').css({ transition: '.2s ease' }).removeClass('py-4');
    } else {
        $('.navbar').removeClass('bg-white shadow-sm fixed-top').css({ transition: '.2s ease' }).addClass('py-4');
    }
}

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