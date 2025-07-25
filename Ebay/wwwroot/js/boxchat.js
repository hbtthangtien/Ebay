/* ==================================================
   Box Chat realtime – KHÔNG mất sự kiện
   ================================================== */
let chatConn;                    // 1 connection duy nhất

async function initBoxChat() {
    const $box = $('#chat-box');
    if (!$box.length) return;

    const productId = +$box.data('product');
    const userId = +$box.data('user');
    const sellerId = +$box.data('seller');

    /* -- đóng connection cũ (nếu khác box) -- */
    if (chatConn) {
        if (chatConn.state !== signalR.HubConnectionState.Disconnected &&
            chatConn.productId === productId &&
            chatConn.sellerId === sellerId) {
            return;                               // Đã đúng box & còn sống → thôi
        }
        try { await chatConn.stop(); } catch { }
    }

    /* -- tạo HubConnection mới -- */
    chatConn = new signalR.HubConnectionBuilder()
        .withUrl('/chathub')          // để mặc định, tránh rủi ro WS
        .withAutomaticReconnect()
        .configureLogging(signalR.LogLevel.Information)
        .build();
    chatConn.productId = productId;               // gắn info để so sau
    chatConn.sellerId = sellerId;

    chatConn.on('ReceiveMessage', m => renderMessage(m, userId));
    chatConn.onreconnected(() => console.info('🔄 Reconnected'));
    chatConn.onclose(e => console.warn('❌ Closed', e));

    try {
        await chatConn.start();
        await chatConn.invoke('JoinBox', productId, userId, sellerId);
        console.log('✅ SignalR connected');
    } catch (e) {
        console.error('🚫 Cannot connect', e);
    }
}

/* ====== delegation – handler không bao giờ mất ====== */
$(document).on('click', '#send-btn', async function () {
    const $input = $('#chat-input');
    const txt = $input.val().trim();
    if (!txt || !chatConn) return;

    const dto = {
        ProductId: chatConn.productId,
        SenderId: $('#chat-box').data('user'),
        ReceiverId: $('#chat-box').data('seller'),
        Content: txt
    };

    try {
        await chatConn.invoke('SendMessage', dto);
        $input.val('');
        const now = new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
        updateSidebarItem(dto.ProductId, dto.ReceiverId, txt, now);  // preview bên “tôi”
    } catch (err) {
        console.error('Send fail', err);
    }
});

$(document).on('keypress', '#chat-input', e => {
    if (e.which === 13) $('#send-btn').click();
});

/* ========== helper ========== */
function renderMessage(m, currentUserId) {
    const me = (m.senderId ?? m.SenderId) === currentUserId;
    const msg = m.content ?? m.Content;
    const time = new Date(m.timestamp || Date.now())
        .toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

    $('#chat-messages')
        .append(`
          <div class="msg-row ${me ? 'msg-me' : 'msg-seller'}">
              <div class="msg-bubble">
                  ${escapeHtml(msg)}
                  <div class="msg-time">${time}</div>
              </div>
          </div>`)
        .scrollTop($('#chat-messages')[0].scrollHeight);

    // cập nhật sidebar cho cả hai phía
    updateSidebarItem(chatConn.productId, chatConn.sellerId, msg, time);
}

function updateSidebarItem(productId, sellerId, preview, time) {
    const $item = $(`.sidebar-item[data-productid='${productId}'][data-sellerid='${sellerId}']`);
    if (!$item.length) return;                    // Box chưa tồn tại ở sidebar

    $item.find('.last-text').text(preview);
    $item.find('.last-time').text(time);

    // Đưa item lên đầu danh sách
    $item.prependTo($item.parent());

    // Đánh dấu active (tuỳ ý)
    $('.sidebar-item').removeClass('active');
    $item.addClass('active');
}

const escapeHtml = s => $('<div>').text(s).html();

/* export global */
window.initBoxChat = initBoxChat;
