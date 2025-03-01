using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Models;
using SQLite;

namespace SchoolApp.Data
{
    public class MusicSchoolDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public MusicSchoolDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Member>().Wait();
            _database.CreateTableAsync<Instrument>().Wait();
            _database.CreateTableAsync<MembruInstrument>().Wait();
            _database.CreateTableAsync<Programare>().Wait();
            _database.CreateTableAsync<Feedback>().Wait();
        }

        public Task<List<Member>> GetMembersAsync()
        {
            return _database.Table<Member>().ToListAsync();
        }

        public Task<int> SaveMemberAsync(Member member)
        {
            if (member.ID != 0)
                return _database.UpdateAsync(member);
            else
                return _database.InsertAsync(member);
        }

        public Task<int> DeleteMemberAsync(Member member)
        {
            return _database.DeleteAsync(member);
        }

        public Task<Member> GetMemberByIdAsync(int id) =>
            _database.Table<Member>().Where(m => m.ID == id).FirstOrDefaultAsync();

        public async Task<List<Member>> GetMembersByRoleAsync(string role)
        {
            return await _database.Table<Member>().Where(m => m.Role == role).ToListAsync();
        }




        public Task<List<Instrument>> GetInstrumentsAsync() => _database.Table<Instrument>().ToListAsync();

        public Task<Instrument> GetInstrumentByIdAsync(int id) =>
            _database.Table<Instrument>().Where(i => i.ID == id).FirstOrDefaultAsync();

        public Task<int> SaveInstrumentAsync(Instrument instrument) =>
            instrument.ID != 0 ? _database.UpdateAsync(instrument) : _database.InsertAsync(instrument);

        public Task<int> DeleteInstrumentAsync(Instrument instrument) => _database.DeleteAsync(instrument);




        public Task<List<MembruInstrument>> GetMembruInstrumentAsync() => _database.Table<MembruInstrument>().ToListAsync();

        public Task<List<MembruInstrument>> GetInstrumentsByMemberIdAsync(int memberId) =>
            _database.Table<MembruInstrument>().Where(mi => mi.MembruID == memberId).ToListAsync();

        public Task<int> SaveMembruInstrumentAsync(MembruInstrument mi) =>
            mi.ID != 0 ? _database.UpdateAsync(mi) : _database.InsertAsync(mi);

        public Task<int> DeleteMembruInstrumentAsync(MembruInstrument mi) => _database.DeleteAsync(mi);




        public Task<List<Programare>> GetProgramariAsync() => _database.Table<Programare>().ToListAsync();

        public Task<Programare> GetProgramareByIdAsync(int id) =>
            _database.Table<Programare>().Where(p => p.ID == id).FirstOrDefaultAsync();

        public Task<int> SaveProgramareAsync(Programare programare) =>
            programare.ID != 0 ? _database.UpdateAsync(programare) : _database.InsertAsync(programare);

        public Task<int> DeleteProgramareAsync(Programare programare) => _database.DeleteAsync(programare);




        public Task<List<Feedback>> GetFeedbacksAsync() => _database.Table<Feedback>().ToListAsync();

        public Task<List<Feedback>> GetFeedbackByProgramareIdAsync(int programareId) =>
            _database.Table<Feedback>().Where(f => f.ProgramareID == programareId).ToListAsync();

        public Task<int> SaveFeedbackAsync(Feedback feedback) =>
            feedback.ID != 0 ? _database.UpdateAsync(feedback) : _database.InsertAsync(feedback);

        public Task<int> DeleteFeedbackAsync(Feedback feedback) => _database.DeleteAsync(feedback);

        public async Task<List<Member>> GetProfessorsAsync()
        {
            return await _database.QueryAsync<Member>(
                "SELECT * FROM Member WHERE UserID IN (SELECT Id FROM User WHERE Role = 'Profesor')"
            );
        }

        public async Task<bool> RegisterUserAsync(
            string email, string password, string nume, string prenume, string telefon, string adresa)
        {
            string hashedPassword = HashPassword(password);

            var member = new Member
            {
                Email = email,
                PasswordHash = hashedPassword,
                Role = "Student",
                Nume = nume,
                Prenume = prenume,
                NumarTelefon = telefon,
                Adresa = adresa,
                CreatedDate = DateTime.UtcNow
            };

            await _database.InsertAsync(member);
            return true;
        }

        public async Task<Member> LoginUserAsync(string email, string password)
        {
            var user = await _database.Table<Member>().Where(m => m.Email == email).FirstOrDefaultAsync();
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }
}
