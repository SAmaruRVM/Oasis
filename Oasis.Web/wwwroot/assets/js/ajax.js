const urlDefaultPath = `${location.protocol}//${location.hostname}:${location.port}/`;
/*let btnDesativoAposSubmissaoForm = undefined;*/

function ajaxCompleto(respostaAjax)
{
    if (respostaAjax.urlRedirecionar !== '')
    {
        location.href = `${urlDefaultPath + respostaAjax.urlRedirecionar}`;
    }
    mostrarNotificacao(respostaAjax);
/*    btnDesativoAposSubmissaoForm.removeAttribute('disabled');*/
}

//document.querySelectorAll('form')
//    .forEach(form => form.addEventListener('submit', function () {
//        btnDesativoAposSubmissaoForm = form.querySelector('button[type="submit"]');
//        btnDesativoAposSubmissaoForm.setAttribute('disabled', '');
//    }));

function mostrarNotificacao(respostaAjax) {
    iziToast.show({
        id: null,
        class: '',
        title: respostaAjax.titulo,
        titleColor: '#000',
        titleSize: '',
        titleLineHeight: '',
        message: respostaAjax.descricao,
        messageColor: '#000',
        messageSize: '',
        messageLineHeight: '',
        backgroundColor: '',
        theme: 'dark', // dark
        color: !(respostaAjax.ocorreuAlgumErro) ? 'green' : 'red',
        icon: !(respostaAjax.ocorreuAlgumErro) ? 'fas fa-check-circle' : 'fas fa-exclamation-circle',
        iconText: '',
        iconColor: '#000',
        iconUrl: null,
        image: '',
        imageWidth: 50,
        maxWidth: null,
        zindex: null,
        layout: 2,
        balloon: false,
        close: true,
        closeOnEscape: false,
        closeOnClick: false,
        displayMode: 0, // once, replace
        position: 'topRight', // bottomRight, bottomLeft, topRight, topLeft, topCenter, bottomCenter, center
        target: '',
        targetFirst: true,
        timeout: 10000,
        rtl: false,
        animateInside: true,
        drag: true,
        pauseOnHover: true,
        resetOnHover: false,
        progressBar: true,
        progressBarColor: '',
        progressBarEasing: 'linear',
        overlay: true,
        overlayClose: true,
        overlayColor: 'rgba(0, 0, 0, 0.6)',
        transitionIn: 'fadeInUp',
        transitionOut: 'fadeOut',
        transitionInMobile: 'fadeInUp',
        transitionOutMobile: 'fadeOutDown',
        buttons: {},
        inputs: {},
        onOpening: function () { },
        onOpened: function () { },
        onClosing: function () { },
        onClosed: function () { }
    });
}