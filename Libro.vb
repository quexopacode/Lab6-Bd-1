Imports System.Data.SqlClient

Public Class Libro

    Private connectionStringBD As String = ("Data Source=localhost;Initial Catalog=Libreria;Integrated Security=True")

    Public Function ObtenerLibros() As DataSet
        Dim dsLibros As New DataSet()
        Try
            Using connection As New SqlConnection(connectionStringBD)
                Dim query As String = "Select Titulo, Autor, CantidadDisponible from Libros"
                Dim adapter As New SqlDataAdapter(query, connection)
                adapter.Fill(dsLibros, "Libros")
            End Using
        Catch ex As Exception
            Throw New Exception("Error al obtener los libros" & ex.Message)
        End Try
        Return dsLibros
    End Function

    Public Sub AgregarLibro(titulo As String, autor As String, cantidad As Integer)
        Try
            Using connection As New SqlConnection(connectionStringBD)
                Dim query As String = "Insert into Libros (Titulo, Autor, CantidadDisponible) values (@Titulo, @Autor, @CantidadDisponible)"
                Dim cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Titulo", titulo)
                cmd.Parameters.AddWithValue("@Autor", autor)
                cmd.Parameters.AddWithValue("@CantidadDisponible", cantidad)
                connection.Open()
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Throw New Exception("Error al agregar libro" & ex.Message)
        End Try
    End Sub

    Public Sub ActualizarLibro(titulo As String, autor As String, cantidad As Integer)
        Try
            Using connection As New SqlConnection(connectionStringBD)
                Dim query As String = "UPDATE Libros SET CantidadDisponible = @CantidadDisponible WHERE Titulo = @Titulo AND Autor = @Autor"
                Dim cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Titulo", titulo)
                cmd.Parameters.AddWithValue("@Autor", autor)
                cmd.Parameters.AddWithValue("@CantidadDisponible", cantidad)
                connection.Open()
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Throw New Exception("Error al actualizar el libro" & ex.Message)
        End Try
    End Sub

    Public Sub EliminarLibro(titulo As String, autor As String)
        Try
            Using connection As New SqlConnection(connectionStringBD)
                Dim query As String = "DELETE FROM Libros WHERE Titulo = @Titulo AND Autor = @Autor"
                Dim cmd As New SqlCommand(query, connection)
                cmd.Parameters.AddWithValue("@Titulo", titulo)
                cmd.Parameters.AddWithValue("@Autor", autor)
                connection.Open()
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            Throw New Exception("Error al eliminar el libro" & ex.Message)
        End Try
    End Sub

End Class
