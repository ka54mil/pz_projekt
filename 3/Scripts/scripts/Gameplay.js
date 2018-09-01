$(document).ready(function () {
    function updatePlayerData(player) {
        $.each(player, function (i, e) {
            $('#Hero_' + i).html(e);
        });
        $('#Hero_Exp_ExpToLvlUp').find('.progress-bar').width(player.Exp/player.ExpToLvlUp*100+'%');
        $('#Hero_AHP_MHP').find('.progress-bar').width(player.AHP / player.MHP * 100 + '%');
        $('#Hero_AMP_MMP').find('.progress-bar').width(player.AMP / player.MMP * 100 + '%');
        $('#Hero_MinDmg_MaxDmg').html(player.MinDmg + ' - ' + player.MaxDmg);
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
            $(this).closest('.card-body').first().scrollTop = $(this).closest('.card-body').first().scrollHeight;
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