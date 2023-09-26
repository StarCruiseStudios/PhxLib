// -----------------------------------------------------------------------------
//  <copyright file="AbstractPhxCollectionTests.cs" company="DangerDan9631">
//      Copyright (c) 2021 DangerDan9631. All rights reserved.
//      Licensed under the MIT License.
//      See https://github.com/Dangerdan9631/Licenses/blob/main/LICENSE-MIT for full license information.
//  </copyright>
// -----------------------------------------------------------------------------

using System.Linq;
using NUnit.Framework;
using Phx.Collections.Data;
using Phx.Test;
using Phx.Validation;

namespace Phx.Collections {
    public abstract partial class AbstractPhxCollectionTests : AbstractPhxCollectionsTestBase {
        [TestCase(5, 3, true)]
        [TestCase(5, 7, false)]
        public void ContainsReturnsExpectedValue(int numElements, int searchElement, bool shouldBeFound) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given(
                "A collection constructed with those elements.",
                () => GetTestInstance<IPhxCollection<string>, string>(elements));
            var search = Given("An item to search for", () => searchElement.ToString());
            var actual = When("The collection is checked for the item", () => collection.Contains(search));

            Then("The expected result is returned", shouldBeFound,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [TestCase(5, 3, true)]
        [TestCase(5, 7, false)]
        public void ContainsReturnsExpectedCovariantValue(int numElements, int searchElement, bool shouldBeFound) {
            var dogs = Given("A collection of dogs", () => from v in Enumerable.Range(0, numElements) select new CovarianceDog(v));
            var collection = Given(
                "An animal collection constructed with those dogs.",
                () => GetTestInstance<IPhxCollection<CovarianceAnimal>, CovarianceDog>(dogs));
            var searchDog = Given("A dog to search for", () => new CovarianceDog(searchElement));
            var searchCat = Given("A cat to search for", () => new CovarianceCat(searchElement));
            var actualDog = When("The collection is checked for the dog", () => collection.Contains(searchDog));
            var actualCat = When("The collection is checked for the cat", () => collection.Contains(searchCat));

            Then("The expected dog result is returned", shouldBeFound,
                (expected) => Verify.That(actualDog.IsEqualTo(expected)));
            Then("The expected cat result is returned", false,
                (expected) => Verify.That(actualCat.IsEqualTo(expected)));
        }

        [TestCase(5, 3, true)]
        [TestCase(5, 7, false)]
        public void ContainsWithPredicateReturnsExpectedValue(int numElements, int minValue, bool shouldBeFound) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given(
                "A collection constructed with those elements.",
                () => GetTestInstance<IPhxCollection<string>, string>(elements));
            var min = Given("A min value to search for", () => minValue.ToString());
            var actual = When("The collection is checked for values greater than the min",
                () => collection.ContainsAny((item) => item.CompareTo(min) >= 0));

            Then("The expected result is returned", shouldBeFound,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [TestCase(5, 0, 3, true)]
        [TestCase(5, 1, 1, true)]
        [TestCase(5, 6, 3, false)]
        public void ContainsAnyReturnsExpectedValue(
            int numElements,
            int minSearchElement,
            int numSearchElements,
            bool shouldBeFound
        ) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given(
                "A collection constructed with those elements.",
                () => GetTestInstance<IPhxCollection<string>, string>(elements));
            var search = Given("A collection of items to search for", () => CreateElements(numSearchElements, minSearchElement));
            var actual = When("The collection is checked for any of the items",
                () => collection.ContainsAny(search));

            Then("The expected result is returned", shouldBeFound,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [TestCase(5, 0, 3, true)]
        [TestCase(5, 1, 1, true)]
        [TestCase(5, 6, 3, false)]
        public void ContainsAnyReturnsExpectedCovariantValue(
            int numElements,
            int minSearchElement,
            int numSearchElements,
            bool shouldBeFound
        ) {
            var dogs = Given("A collection of dogs", () => from v in Enumerable.Range(0, numElements) select new CovarianceDog(v));
            var collection = Given(
                "An animal collection constructed with those dogs.",
                () => GetTestInstance<IPhxCollection<CovarianceAnimal>, CovarianceDog>(dogs));
            var searchDogs = Given("A collection of dogs to search for",
                () => from v in Enumerable.Range(minSearchElement, numSearchElements) select new CovarianceDog(v) as CovarianceAnimal);
            var searchCats = Given("A collection of cats to search for",
                () => from v in Enumerable.Range(minSearchElement, numSearchElements) select new CovarianceCat(v) as CovarianceAnimal);

            var actualDogs = When("The collection is checked for any of the dogs",
                () => collection.ContainsAny(searchDogs));
            var actualCats = When("The collection is checked for any of the cats",
                () => collection.ContainsAny(searchCats));

            Then("The expected dog result is returned", shouldBeFound,
                (expected) => Verify.That(actualDogs.IsEqualTo(expected)));
            Then("The expected result is returned", false,
                (expected) => Verify.That(actualCats.IsEqualTo(expected)));
        }

        [TestCase(5, 0, 3, true)]
        [TestCase(5, 0, 5, true)]
        [TestCase(5, 1, 1, true)]
        [TestCase(5, 3, 5, false)]
        [TestCase(5, 6, 3, false)]
        public void ContainsAllReturnsExpectedValue(
            int numElements,
            int minSearchElement,
            int numSearchElements,
            bool shouldBeFound
        ) {
            var elements = Given("A collection of elements", () => CreateElements(numElements));
            var collection = Given(
                "A collection constructed with those elements.",
                () => GetTestInstance<IPhxCollection<string>, string>(elements));
            var search = Given("A collection of items to search for", () => CreateElements(numSearchElements, minSearchElement));
            var actual = When("The collection is checked for all of the items",
                () => collection.ContainsAll(search));

            Then("The expected result is returned", shouldBeFound,
                (expected) => Verify.That(actual.IsEqualTo(expected)));
        }

        [TestCase(5, 0, 3, true)]
        [TestCase(5, 0, 5, true)]
        [TestCase(5, 1, 1, true)]
        [TestCase(5, 3, 5, false)]
        [TestCase(5, 6, 3, false)]
        public void ContainsAllReturnsExpectedCovariantValue(
            int numElements,
            int minSearchElement,
            int numSearchElements,
            bool shouldBeFound
        ) {
            var dogs = Given("A collection of dogs", () => from v in Enumerable.Range(0, numElements) select new CovarianceDog(v));
            var collection = Given(
                "An animal collection constructed with those dogs.",
                () => GetTestInstance<IPhxCollection<CovarianceAnimal>, CovarianceDog>(dogs));
            var searchDogs = Given("A collection of dogs to search for",
                () => from v in Enumerable.Range(minSearchElement, numSearchElements) select new CovarianceDog(v) as CovarianceAnimal);
            var searchCats = Given("A collection of cats to search for",
                () => from v in Enumerable.Range(minSearchElement, numSearchElements) select new CovarianceCat(v) as CovarianceAnimal);

            var actualDogs = When("The collection is checked for any of the dogs",
                () => collection.ContainsAll(searchDogs));
            var actualCats = When("The collection is checked for any of the cats",
                () => collection.ContainsAll(searchCats));

            Then("The expected dog result is returned", shouldBeFound,
                (expected) => Verify.That(actualDogs.IsEqualTo(expected)));
            Then("The expected result is returned", false,
                (expected) => Verify.That(actualCats.IsEqualTo(expected)));
        }
    }
}