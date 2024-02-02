﻿
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Services
{
    public class SpecificationService
    {
        private readonly SpecificationRepository _specificationRepository;
        private readonly ProductRepository _productRepository;
        public SpecificationService(SpecificationRepository ss, ProductRepository productRepository)
        {
            _specificationRepository = ss;
            _productRepository = productRepository;
        }

        public SpecificationEntity CreateSpecification(SpecificationEntity product)
        {
            try
            {
                if (_productRepository.Exists(x => x.ArticleNumber == product.ArticleNumber))
                {
                    var created = _specificationRepository.AddToDB(product);
                    return created;
                }
            }
            catch (Exception e) { Debug.WriteLine("ERROR :: " + e); }
            return null!;
        }

        public SpecificationEntity GetSpecification(string value)
        {
            try
            {
                var product = _specificationRepository.GetOneFromDB(x => x.ArticleNumber == value);
                if (product != null)
                    return product;
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return null!;
        }

        public bool UpdateSpecification(SpecificationEntity entity)
        {
            try
            {
                if (!_specificationRepository.Exists(x => x.ArticleNumber == entity.ArticleNumber))
                {
                    var created = CreateSpecification(entity);
                    if (created != null)
                        return true;
                }
                else
                {
                    var updated = _specificationRepository.UpdateEntityInDB(entity, x => x.ArticleNumber == entity.ArticleNumber);
                    if (updated != null)
                        return true;
                }

            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }

        public bool DeleteSpecification(string articleNumber)
        {
            try
            {
                var deleted = _specificationRepository.DeleteFromDB(x => x.ArticleNumber == articleNumber);
                if (deleted)
                    return true;
            }
            catch (Exception e) { Debug.WriteLine("Error: " + e.Message); }
            return false;
        }
    }
}
