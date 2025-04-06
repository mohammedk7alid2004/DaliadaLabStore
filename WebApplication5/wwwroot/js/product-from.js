$(document).ready(function () {
	$("#Cover").on('change', function () {
		$(".cover-pre").attr('src', window.URL.createObjectURL(this.files[0])).removeClass("d-none");
	});
});