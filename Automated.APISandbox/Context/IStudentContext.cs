using Automated.APISandbox.Model;

namespace Automated.APISandbox.Context;

public interface IStudentContext
{
    Task<IEnumerable<Student>> GetStudentsAsync();

    Task<Student?> GetStudentByIdAsync(int id);

    Task<Student> AddStudentAsync(Student student);

    Task<bool> UpdateStudentAsync(Student student);

    Task<bool> DeleteStudentAsync(int id);
}
