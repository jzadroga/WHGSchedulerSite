$(document).ready(function () {

    $('.edit-sponsor').click(function (event) {
        var sponsor = {
            name: $(this).attr('data-name'),
            id: $(this).attr('data-id'),
            logo: $(this).attr('data-logo')
        };

        if (sponsor.id === '0') {
            $('#logoView').hide();
            $('#sponsorLogoFile').show();
        } else {
            $('#logoView').show();
            $('#sponsorLogoFile').hide();
        }

        $('#sponsorName').val(sponsor.name);
        $('#sponsorID').val(sponsor.id);
        $('#sponsorLogo').val(sponsor.logo);

        $('#mdl-sponsor').modal('show');
    });

    $('#modifiyLogo').click(function (event) {
        $('#logoView').hide();
        $('#sponsorLogoFile').show();
        return false;
    });
});