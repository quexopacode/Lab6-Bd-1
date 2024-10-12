

Public Class Form1
    Dim Libro As New Libro
    Private Sub txtCantidad_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ChrW(8) Then
            e.Handled = True
        End If
    End Sub

    Private Sub CargarLibros()
        Dim ds As New DataSet()
        Try
            ds = Libro.ObtenerLibros()
            DataGridView1.DataSource = ds.Tables("Libros")
            DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            DataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
            Me.AutoSize = True
        Catch ex As Exception
            MessageBox.Show("Error al cargar los libros: " & ex.Message)
        End Try
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarLibros()
        btnAgregar.Enabled = True
        btnActualizar.Enabled = False
        btnActualizar.Visible = False
        btnEliminar.Enabled = False
        btnEliminar.Visible = False
        btnNuevoLibro.Enabled = True
        lblInfo.Text = ""
    End Sub

    Private Sub btnNuevoLibro_Click(sender As Object, e As EventArgs) Handles btnNuevoLibro.Click
        txtTitulo.Clear()
        txtAutor.Clear()
        txtCantidad.Clear()
        txtTitulo.Enabled = True
        txtAutor.Enabled = True
        txtTitulo.ReadOnly = False
        txtAutor.ReadOnly = False
        btnAgregar.Enabled = True
        btnAgregar.Visible = True
        btnActualizar.Enabled = False
        btnActualizar.Visible = False
        btnEliminar.Enabled = False
        btnEliminar.Visible = False
        btnNuevoLibro.Enabled = True
        lblInfo.text = ""
    End Sub


    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim titulo As String
            Dim autor As String
            Dim cantidad As Integer
            titulo = txtTitulo.Text
            autor = txtAutor.Text
            cantidad = CInt(txtCantidad.Text)
            If String.IsNullOrEmpty(titulo) OrElse String.IsNullOrEmpty(autor) OrElse cantidad <= 0 Then
                MsgBox("Por favor, complete todos los campos y asegúrese de que la cantidad sea positiva.")
            Else
                Libro.AgregarLibro(titulo, autor, cantidad)
                MsgBox("Libro agregado con éxito.")
                CargarLibros()
            End If
        Catch ex As Exception
            MsgBox("Error al agregar libro " & ex.Message)
        Finally
            txtTitulo.Clear()
            txtAutor.Clear()
            txtCantidad.Clear()
        End Try
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            txtTitulo.Text = row.Cells("Titulo").Value.ToString()
            txtAutor.Text = row.Cells("Autor").Value.ToString()
            txtCantidad.Text = row.Cells("CantidadDisponible").Value.ToString()
            txtTitulo.ReadOnly = True
            txtAutor.ReadOnly = True
            btnAgregar.Enabled = False
            btnAgregar.Visible = False
            btnActualizar.Visible = True
            btnActualizar.Enabled = True
            btnEliminar.Visible = True
            btnEliminar.Enabled = True
            btnNuevoLibro.Enabled = True
            lblInfo.Text = "Click para agregar libro -->"
        End If
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        Try
            Dim titulo As String
            Dim autor As String
            Dim cantidad As Integer
            titulo = txtTitulo.Text
            autor = txtAutor.Text
            cantidad = CInt(txtCantidad.Text)
            If String.IsNullOrEmpty(titulo) OrElse String.IsNullOrEmpty(autor) OrElse cantidad <= 0 Then
                MsgBox("Por favor, complete todos los campos con datos válidos.")
                Exit Sub
            End If
            Libro.ActualizarLibro(titulo, autor, cantidad)
            MsgBox("Libro actualizado correctamente.")
            CargarLibros()
        Catch ex As Exception
            MsgBox("Error al actualizar el libro: " & ex.Message)
        End Try

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Try
            Dim titulo As String
            Dim autor As String
            titulo = txtTitulo.Text
            autor = txtAutor.Text
            If String.IsNullOrEmpty(titulo) OrElse String.IsNullOrEmpty(autor) Then
                MsgBox("Por favor, seleccione un libro correcto")
            Else
                Libro.EliminarLibro(titulo, autor)
                MsgBox("Libro eliminado correctamente.")
                CargarLibros()
            End If
        Catch ex As Exception
            MsgBox("Error al eliminar el libro: " & ex.Message)
        End Try

    End Sub
End Class
