$(document).ready(function () {
    function updatePlayerData(player) {
        $.each(player, function (i, e) {
            $('#Hero_' + i).html(e);
        });
        $('#Hero_Exp_ExpToLvlUp').html(player.Exp+'/'+player.ExpToLvlUp);
        $('#Hero_MinDmg_MaxDmg').html(player.MinDmg + ' - ' + player.MaxDmg);
        $('#Hero_AHP_MHP').html(player.AHP + '/' + player.MHP);
        $('#Hero_AMP_MMP').html(player.AMP + '/' + player.MMP);
    }
    $('#type-action-input').on('keypress', function (e) {
        if (e.which === 13) {
            $(this).attr('disabled', 'disabled');
            $.post({
                url: '/Gameplay/ExecuteAction',
                data: {
                    action: $(this).val(),
                    heroId: $('#ID').val()
                }
            }).then(function (jsonResult) {
                let result = JSON.parse(jsonResult);
                $.each(result.Messages, function (i,e) {
                    $('#events-container').append('<div class=\"col-12\">' + e + '</div>');
                });
                updatePlayerData(result.Player);
            });
            $(this).val('');
            $(this).removeAttr('disabled');
        }
    });

    $('.delete-button').on('click', function () {
        let $this = $(this);
        $($this.data('target')).find('#confirm-modal').data('url', $this.data('href')).data('id', $this.data('id'));
    });

    $('#confirm-modal').on('click', function () {
        let $this = $(this);
        $.ajax({
            url: $this.data('url'),
            type: 'DELETE',
            data: {
                heroId: $this.data('id')
            },
            statusCode: {
                200 : function(response) {
                    $('#character-' + $this.data('id')).remove();
                }
            }
        });
    });
});