function submitForm() {
	var formData = new FormData(); // Crear objeto FormData para enviar datos

	var tableRows = document.querySelectorAll('table tbody tr');

	tableRows.forEach(function (row, index) {
		// Obtener datos de cada fila

		var Result = row.querySelector('select[name= "SubCategory.Result"]').value;
		var Comment = row.querySelector('input[name="SubCategory.Comment"]').value;
		var SubCategory_id = row.querySelector('input[name="SubCategory.SubCategory_id"]').value;
		var Function_name = row.querySelector('input[name="SubCategory.Function_name"]').value;
		var Category_name = row.querySelector('input[name="SubCategory.Category_name"]').value;
		var Plant_description = row.querySelector('input[name="SubCategory.Plant_description"]').value;

		// Agregar datos de fila al FormData

		formData.append('subCategories[' + index + '].Result', Result);
		formData.append('subCategories[' + index + '].Comment', Comment);
		formData.append('subCategories[' + index + '].SubCategory_id', SubCategory_id);
		formData.append('subCategories[' + index + '].Function_name', Function_name);
		formData.append('subCategories[' + index + '].Category_name', Category_name);
		formData.append('subCategories[' + index + '].Plant_description', Plant_description);


		// Obtener el archivo seleccionado
		var fileInputId = 'real-file_' + SubCategory_id;
		var fileInput = document.getElementById(fileInputId);
		var file = fileInput.files[0];

		// Agregar el archivo al FormData
		formData.append('files', file);
	});
	formData.forEach(function (value, key) {
		console.log(key, value);
	});
	// Enviar FormData al servidor utilizando AJAX
	$.ajax({
		url: $('#myForm').attr('action'),
		type: 'POST',
		data: formData, // Enviar FormData
		processData: false, // No procesar los datos (FormData ya está formateado correctamente)
		contentType: false,
		success: function (response) {
			window.location.href = "https://localhost:7154/Security";
		},
		error: function (xhr, status, error) {

		}
	});
}

