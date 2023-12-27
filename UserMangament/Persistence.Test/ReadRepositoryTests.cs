using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserMangament.Persistence.Contexts;
using UserMangament.Persistence.Repositories.Abstracts;
using Xunit;

namespace Persistence.Test
{
    public class ReadRepositoryTests
    {
        //[Fact]
        //public async Task AnyAsync_WithPredicate_ReturnsExpectedResult()
        //{
        //    // Arrange
        //    var mockDbSet = new Mock<DbSet<User>>();
        //    mockDbSet.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<User, bool>>>()))
        //        .ReturnsAsync(true);

        //    var mockContext = new Mock<User>();
        //    mockContext.Setup(c => c.Set<User>()).Returns(mockDbSet.Object);

        //    var repository = new ReadRepository<User, TestContext>(mockContext.Object);

        //    // Act
        //    var result = await repository.AnyAsync(e => e.Id == 1); // Replace with your predicate

        //    // Assert
        //    Assert.True(result);
        //}

     
    }

  

  
}
