$(function(){
	
	function _load(content){
		
		var that = this;
		
		that.element = content;

		content.find('.btn_enviar').bind('click',$.proxy(_enviar, that));
		
	}

	function _enviar(){

		var that = this;

		var url = that.element.find('#url').val();

		if(!url){
			alert('Preecha o campo URL!');
			return;
		}

		$.post(window.location.origin + '/home/enviar',{ url: url}).then(function(response){ 
			
			var artistas = $(response.html).find('#browselist').find('li:not(:first)'). filter(function(){
				return this.textContent.indexOf(';') < 0;
			}).map(function(){ 
				return this.textContent;
			});
			
			return _salvaLista(JSON.parse(JSON.stringify(artistas)));

		}).done(function(response){

			alert('tranquilo Favoravel');

		});

	}

	function _salvaLista(listaArtistas) { 
		
		return $.post(window.location.origin + '/home/salvarlista', { artistas: listaArtistas });
	}

	_load($('#cruzeiro'));
	

	
});
